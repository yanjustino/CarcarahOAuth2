using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carcarah.OnAuth.OpenId.AuthenticationFlow;
using Carcarah.OnAuth.OpenId.Request;
using Microsoft.Owin;
using Carcarah.OnAuth.OpenId.AuthenticationFlow.Flows;

namespace Carcarah.OnAuth.Tests.OpenId.AuthenticationFlow
{
    [TestClass]
    public class AuthenticationFlowFactoryTest
    {
        private OwinContext context = new OwinContext();

        [TestMethod]
        public void NewInstanceOfAuthorizationCodeFlow()
        {
            var response_type = "code";
            var query = $"response_type={response_type}&"+
                         "scope=openid profile email&" + 
                         "client_id=s6BhdRkqt3&"+
                         "state=af0ifjsldkj&" +
                         "redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb";

            context.Request.QueryString = new QueryString(query);

            var request = new AuthenticationRequest(context);
            var flow = AuthenticationFlowFactory.New(context, null);

            Assert.IsInstanceOfType(flow, typeof(AuthorizationCodeFlow));
        }

        [TestMethod]
        public void NewInstanceOfImplicitFlow()
        {
            var response_type = "id_token";
            var query = $"response_type={response_type}&" +
                         "scope=openid profile email&" +
                         "client_id=s6BhdRkqt3&" +
                         "state=af0ifjsldkj&" +
                         "redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb";

            context.Request.QueryString = new QueryString(query);
            var request = new AuthenticationRequest(context);
            var flow = AuthenticationFlowFactory.New(context, null);

            Assert.IsInstanceOfType(flow, typeof(ImplicitFlow));
        }

        [TestMethod]
        public void NewInstanceOfImplicitFlow2()
        {
            var response_type = "id_token token";
            var query = $"response_type={response_type}&" +
                         "scope=openid profile email&" +
                         "client_id=s6BhdRkqt3&" +
                         "state=af0ifjsldkj&" +
                         "redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb";

            context.Request.QueryString = new QueryString(query);
            var request = new AuthenticationRequest(context);
            var flow = AuthenticationFlowFactory.New(context, null);

            Assert.IsInstanceOfType(flow, typeof(ImplicitFlow));
        }

        [TestMethod]
        public void NewInstanceOfHybridFlow()
        {
            var response_type = "code id_token";
            var query = $"response_type={response_type}&" +
                         "scope=openid profile email&" +
                         "client_id=s6BhdRkqt3&" +
                         "state=af0ifjsldkj&" +
                         "redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb";

            context.Request.QueryString = new QueryString(query);
            var request = new AuthenticationRequest(context);
            var flow = AuthenticationFlowFactory.New(context, null);

            Assert.IsInstanceOfType(flow, typeof(HybridFlow));
        }

        [TestMethod]
        public void NewInstanceOfHybridFlow2()
        {
            var response_type = "code token";
            var query = $"response_type={response_type}&" +
                         "scope=openid profile email&" +
                         "client_id=s6BhdRkqt3&" +
                         "state=af0ifjsldkj&" +
                         "redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb";

            context.Request.QueryString = new QueryString(query);
            var request = new AuthenticationRequest(context);
            var flow = AuthenticationFlowFactory.New(context, null);

            Assert.IsInstanceOfType(flow, typeof(HybridFlow));
        }

        [TestMethod]
        public void NewInstanceOfHybridFlow3()
        {
            var response_type = "code id_token token";
            var query = $"response_type={response_type}&" +
                         "scope=openid profile email&" +
                         "client_id=s6BhdRkqt3&" +
                         "state=af0ifjsldkj&" +
                         "redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb";

            context.Request.QueryString = new QueryString(query);
            var request = new AuthenticationRequest(context);
            var flow = AuthenticationFlowFactory.New(context, null);

            Assert.IsInstanceOfType(flow, typeof(HybridFlow));
        }
    }
}
