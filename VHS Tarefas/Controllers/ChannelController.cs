using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHS_Tarefas.Entities;
using VHS_Tarefas.Services;

namespace VHS_Tarefas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private IChannelService _channelService;
        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet("")]
        public async Task<List<ChannelEntity>> Get()
        {
            return await _channelService.ListAll();
        }

        [HttpPost("")]
        public async Task<ChannelEntity> Get(ChannelEntity entity)
        {
            return await _channelService.Save(entity);
        }
    }
}
