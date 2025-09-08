using System;

namespace Qrist.Domain
{
    public class SessionStateItem
    {
        public string State { get; set; }

        public Guid Id { get; set; }

        public string QrCodeData { get; set; }
        
        public string AccessToken { get; set; }
    }
}