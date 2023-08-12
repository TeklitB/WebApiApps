using AutoMapper;
using CoinDeskWebApiApp.interfaces;
using CoinDeskWebApiApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CoinDeskWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinDesksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientService _httpClientService;
        private readonly IHttpClientFactoryService _httpClientFactoryService;

        public CoinDesksController(IHttpClientService httpClientService, 
            IHttpClientFactoryService httpClientFactoryService,
            IMapper mapper) 
        { 
            _httpClientFactoryService = httpClientFactoryService;
            _httpClientService = httpClientService;
            _mapper = mapper;
        }

        [HttpGet("GetWithHttpClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithHttpClient()
        {
            try
            {
                var btcContent = await _httpClientService.GetBitCoinContent();

                return Ok(_mapper.Map<BitCoinDto>(btcContent));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetWithIHttpClientFactory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithWithIHttpClientFactory()
        {
            try
            {
                var btcContent = await _httpClientFactoryService.GetBitCoinContent();

                return Ok(_mapper.Map<BitCoinDto>(btcContent));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetWithIHttpClientFactoryWithNamedClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithIHttpClientFactoryWithNamedClient()
        {
            try
            {
                var btcContent = await _httpClientFactoryService.GetBitCoinContentWithUsing();

                return Ok(_mapper.Map<BitCoinDto>(btcContent));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Obsolete]
        [HttpGet("GetWithHttpClientWithUsing")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithHttpClientWithUsing()
        {
            try
            {
                var btcContent = await _httpClientService.GetBitCoinContentWithUsing();

                return Ok(_mapper.Map<BitCoinDto>(btcContent));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
