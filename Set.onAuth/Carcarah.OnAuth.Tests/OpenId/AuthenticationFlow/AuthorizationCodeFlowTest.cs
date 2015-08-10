using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carcarah.OnAuth.OpenId.Request;
using System.Threading.Tasks;
using Microsoft.Owin;
using Carcarah.OnAuth.Config;
using Carcarah.OnAuth.OpenId.AuthenticationFlow;
using System.IO;

namespace Carcarah.OnAuth.Tests
{
    [TestClass]
    public class AuthorizationCodeFlowTest
    {
        OwinContext context = new OwinContext(new Dictionary<string, object>());

        [TestMethod]
        [ExpectedException(typeof(AuthenticationRequestException))]
        public void validate_all_the_oauth_2_parameters()
        {
            context.Request.QueryString = new Microsoft.Owin.QueryString("test=test");
            var request = new AuthenticationRequest(context);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationRequestException))]
        public void verify_that_a_scope_parameter_is_present()
        {
            context.Request.QueryString = new Microsoft.Owin.QueryString("response_type=code &client_id=s6BhdRkqt3 &state=af0ifjsldkj &redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb");
            var request = new AuthenticationRequest(context);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationRequestException))]
        public void verify_that_a_scope_parameter_contains_the_openid_scope_value()
        {
            context.Request.QueryString = new Microsoft.Owin.QueryString("response_type=code &scope= profile email &client_id=s6BhdRkqt3 &state=af0ifjsldkj &redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb");
            var request = new AuthenticationRequest(context);
        }

        [TestMethod]
        public void verify_that_a_scope_parameter_is_present_and_contains_the_openid_scope_value()
        {
            context.Request.QueryString = new Microsoft.Owin.QueryString("response_type=code &scope=openid profile email &client_id=s6BhdRkqt3 &state=af0ifjsldkj &redirect_uri=https%3A%2F%2Fclient.example.org%2Fcb");
            var request = new AuthenticationRequest(context);

            Assert.IsNotNull(request.Query.scope);
            Assert.AreEqual("openid profile email ", request.Query.scope);
        }
    }
}
