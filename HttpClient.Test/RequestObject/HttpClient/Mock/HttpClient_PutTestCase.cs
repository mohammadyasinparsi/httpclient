using RabaniHttpClient.XUnitTest.RequestObject.HttpClient.Object;
using System.Collections;
using System.Collections.Generic;

namespace HttpClient.XUnitTest.RequestObject.HttpClient.Mock
{
    public class HttpClient_PutTestCase : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new XunitPostObject
            {
                    id = 1,
                title = "foo",
                body = "bar",
                userId = 1
            }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
