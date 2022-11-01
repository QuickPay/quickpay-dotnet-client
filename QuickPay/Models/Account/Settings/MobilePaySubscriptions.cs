namespace Quickpay.Models.Account.Settings
{
    public class MobilePaySubscriptions
    {
        public bool active { get; set; }
        public string auth_state { get; set; }
        public string code_verifier { get; set; }
        public int last_token_refresh { get; set; }
        public string nonce { get; set; }
        public int pending_status { get; set; }
        public object provider { get; set; }
        public string refresh_token { get; set; }
    }
}