using RabaniHttpClient.XUnitTest.RequestObject.HttpClient.Object;
using System.Collections;
using System.Collections.Generic;

namespace HttpClient.XUnitTest.RequestObject.HttpClient.Mock
{
    public class HttpClient_PostTestCase : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new XunitPostObject
            {

                userId = 1,
                title = "foo",
                body = "bar"
            }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
