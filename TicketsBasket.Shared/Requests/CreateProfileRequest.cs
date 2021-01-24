using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsBasket.Shared.Requests
{
    public class CreateProfileRequest
    {
        public bool IsOrganizer { get; set; }

        public IFormFile Picture { get; set; }
    }
}
