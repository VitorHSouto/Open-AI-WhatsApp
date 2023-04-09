using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHS_Tarefas.Domain.Messages.CloudAPI;
using VHS_Tarefas.Services;

namespace VHS_Tarefas.Controllers
{
    [Route("api/cloud")]
    [ApiController]
    public class CloudAPIController : ControllerBase
    {
        private IChannelService _channelService;
        public CloudAPIController(IChannelService channelService) 
        { 
            _channelService = channelService;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> Post(CloudAPIWebhook webhook)
        {
            await _channelService.HandleWebhook(webhook);

            return Ok();
        }

        [HttpGet("webhook")]
        public ActionResult Challenge(CloudAPIWebhookChallenge hub)
        {
            return Ok(hub.Challenge);
        }
    }
}
