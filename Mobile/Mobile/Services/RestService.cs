using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Mobile.Services
{
  internal class RestService
  {
    private readonly HttpClient _client;

    public RestService()
    {
      _client = new HttpClient();
    }
  }
}