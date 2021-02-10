using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Povorot.Api.Dto;
using Povorot.DAL.Models;
using Povorot.DAL.Repository;
using Serilog;

namespace Povorot.Api.Controllers
{
    [Route("api/v1/CarStations")]
    public class CarStationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<CarStationController> _logger;

        private readonly IMapper _mapper;

        public CarStationController(IUnitOfWork unitOfWork, ILogger<CarStationController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение списка станций
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var carStations = await _unitOfWork.CarStations.List(includes: new List<string>()
                         {
                             nameof(CarStation.RepairPosts)
                         });
            return Ok(_mapper.Map<List<CarStationDto>>(carStations));
        }

        /// <summary>
        /// Получение одной станции по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор станции</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var station = await _unitOfWork.CarStations.Get(d => d.Id == id, includes: new List<string>()
            {
                nameof(CarStation.RepairPosts)
            });
            if (station == null)
                return NotFound();
            return Ok(_mapper.Map<CarStationDto>(station));
        }

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="requestDto">Объект с данными для создания</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarStationCreateDto requestDto)
        {
            if (ModelState.IsValid)
            {
                CarStation station = _mapper.Map<CarStation>(requestDto);
                await _unitOfWork.CarStations.Create(station);
                await _unitOfWork.Save(1);
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <param name="requestDto">Объект с данными</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] CarStationDto requestDto)
        {
            if (id != requestDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                CarStation station = await _unitOfWork.CarStations.Get(d => d.Id == id);
                if (station == null)
                    return NotFound();
                station.Address = requestDto.Address;
                station.Name = requestDto.Name;
                station.EndWorkTime = requestDto.EndWorkTime;
                station.StartWorkTime = requestDto.StartWorkTime;
                _unitOfWork.CarStations.Update(station);
                await _unitOfWork.Save(1);
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(long id)
        {
            CarStation station = await _unitOfWork.CarStations.Get(d => d.Id == id);
            if (station == null)
                return NotFound();
            _unitOfWork.CarStations.Delete(station);
            await _unitOfWork.Save(1);
            return Ok();
        }
        
        /// <summary>
        /// Получение информации по боксу в сервисе
        /// </summary>
        /// <param name="id">Идентификатор сервиса</param>
        /// <param name="postId">Идентификатор бокса</param>
        /// <returns></returns>
        [HttpGet("{id}/RepairPost/{postId}")]
        [ProducesResponseType(typeof(RepairPostDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRepairPosts(long id, long postId)
        {
            CarStation station = await _unitOfWork.CarStations.Get(d => d.Id == id, new List<string>()
            {
                nameof(CarStation.RepairPosts)
            });
            if (station == null)
                return NotFound();

            var post = station.RepairPosts.FirstOrDefault(d => d.Id == postId);
            return Ok(_mapper.Map<RepairPostDto>(post));
        }

        /// <summary>
        /// Добавление бокса в сервис
        /// </summary>
        /// <param name="id">Идентификатор сервиса</param>
        /// <param name="requestDto">Объект с данными для добавления</param>
        /// <returns></returns>
        [HttpPost("{id}/RepairPost")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRepairPost(long id, [FromBody] RepairPostCreateDto requestDto)
        {
            CarStation station = await _unitOfWork.CarStations.Get(d => d.Id == id, new List<string>()
            {
                nameof(CarStation.RepairPosts)
            });
            if (station == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(JsonConvert.SerializeObject(ModelState.Values));

            if (station.RepairPosts.Any(d => d.Name.Equals(requestDto.Name.Trim().ToLower())))
            {
                return BadRequest($"Станция обслуживания {station.Name} уже содержит пост с именем {requestDto.Name}");
            }

            RepairPost post = new()
            {
                Name = requestDto.Name,
                CarStation = station
            };
            station.RepairPosts.Add(post);
            _unitOfWork.CarStations.Update(station);
            await _unitOfWork.Save(1);
            return Ok();
        }

        /// <summary>
        /// Удаление бокса из сервиса
        /// </summary>
        /// <param name="id">Идентификатор сервиса</param>
        /// <param name="postId">Идентификатор бокса для удаления</param>
        /// <returns></returns>
        [HttpDelete("{id}/RepairPost/{postId}")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRepairPost(long id, long postId)
        {
            CarStation station = await _unitOfWork.CarStations.Get(d => d.Id == id, new List<string>()
            {
                nameof(CarStation.RepairPosts)
            });
            if (station == null)
                return NotFound();

            var post = station.RepairPosts.FirstOrDefault(d => d.Id == postId);
            if (post == null)
                return NotFound();
            station.RepairPosts.Remove(post);
            _unitOfWork.RepairPosts.Delete(post);
            await _unitOfWork.Save(1);
            return Ok();
        }
        
        /// <summary>
        /// Обновление информации о боксе
        /// </summary>
        /// <param name="id">Идентификатор сервиса</param>
        /// <param name="postId">Идентификатор бокса</param>
        /// <param name="requestDto">Данные для обновления</param>
        /// <returns></returns>
        [HttpPut("{id}/RepairPost/{postId}")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRepairPost(long id, long postId, [FromBody] RepairPostCreateDto requestDto)
        {
            CarStation station = await _unitOfWork.CarStations.Get(d => d.Id == id, new List<string>()
            {
                nameof(CarStation.RepairPosts)
            });
            if (station == null)
                return NotFound();

            var post = station.RepairPosts.FirstOrDefault(d => d.Id == postId);
            if (post == null)
                return NotFound();
            post.Name = requestDto.Name;
            _unitOfWork.RepairPosts.Update(post);
            await _unitOfWork.Save(1);
            return Ok();
        }
    }
}
