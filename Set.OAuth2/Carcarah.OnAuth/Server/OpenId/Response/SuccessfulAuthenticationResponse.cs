namespace Carcarah.OnAuth.Server.OpenId.Response
{
    public class SuccessfulAuthenticationResponse
    {
        public string code { get; set; }
        public string state { get; set; }

        public override string ToString()
        {
            return $"code={code}&state={state}";
        }
    }
}
