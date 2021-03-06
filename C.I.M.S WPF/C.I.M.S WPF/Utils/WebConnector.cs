﻿using C.I.M.S_WPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Utils
{
    public class WebConnector
    {
        private readonly BackgroundWorker _worker;
        private bool _restart = false;

        public WebConnector()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
        }

        public IEnumerable<T> CallAPI<T>(string baseAddress, string requestUri, string workDescription = "")
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);

            // JSON 형식에 대한 Accept 헤더를 추가합니다.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Messenger.Instance.Send(true, Context.PROGRESSBAR);
            Messenger.Instance.Send(workDescription, Context.PROGRESS_DESC);
            // 모든 제품들의 목록.
            HttpResponseMessage response = client.GetAsync(requestUri).Result;  // 호출 블록킹!
            if (response.IsSuccessStatusCode)
            {
                // 응답 본문 파싱. 블록킹!
                var results = response.Content.ReadAsAsync<IEnumerable<T>>().Result;

                Messenger.Instance.Send(false, Context.PROGRESSBAR);
                Messenger.Instance.Send("", Context.PROGRESS_DESC);
                return results;
            }
            else
            {
                Messenger.Instance.Send(false, Context.PROGRESSBAR);
                Messenger.Instance.Send("", Context.PROGRESS_DESC);
                Messenger.Instance.Send($"{(int)response.StatusCode} ({response.ReasonPhrase})", Context.PROGRESS_DESC);
                return null;
            }
        }

        public void CallAPIAsync<T>(string baseAddress, string requestUri, int time, Action<IEnumerable<T>> progressChanged)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _worker.DoWork +=
                    (sender, e) => 
                    {
                        int count = 0;
                        while(true)
                        {
                            if(_worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                            else
                            {
                                Thread.Sleep(time);
                                HttpResponseMessage response = client.GetAsync(requestUri).Result;
                                IEnumerable<T> result = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                                _worker.ReportProgress(++count, result);
                            }
                        }
                    };

            _worker.ProgressChanged += (sender, e) => { progressChanged((IEnumerable<T>)e.UserState); };

            _worker.RunWorkerCompleted += 
                    (sender, e) => 
                    {
                        if (e.Cancelled)
                        {
                            Messenger.Instance.Send("Call API Async Cancelled.", Context.WRITE_LOG);
                        }
                        else if (e.Error != null)
                        {
                            Messenger.Instance.Send("Call API Async Exception Thrown.", Context.WRITE_LOG);
                            _restart = true;
                        }
                        else
                        {
                            Messenger.Instance.Send("Call API Async Finished.", Context.WRITE_LOG);
                        }

                        if (_restart)
                        {
                            _worker.RunWorkerAsync();
                            _restart = false;
                        }
                        else
                        {
                            _worker.Dispose();
                        }
                    };

            _worker.RunWorkerAsync();
        }

        public void CancelAsyncCall()
        {
            _worker.CancelAsync();
        }
    }
}
