using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Povorot.Api.Dto;
using Povorot.DAL.Models;
using Povorot.DAL.Repository;

namespace Povorot.Api.Controllers
{
    [Route("api/v1/Mechanics")]
    public class MechanicController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MechanicController> _logger;

        public MechanicController(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<MechanicController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Получение списка мастеров
        /// </summary>
        /// <param name="carStationId">фильтр по сервису - 0 для всех</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<MechanicDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List(long carStationId)
        {
            var mechanics = await _unitOfWork.Mechanics
                .List(d => carStationId == 0 || d.CarStationId == carStationId,
                    x => x.OrderBy(s => s.CarStationId).ThenBy(s => s.Id),
                    new List<string>() {nameof(Mechanic.CarStation)});
            return Ok(_mapper.Map<List<MechanicDto>>(mechanics));
        }

        /// <summary>
        /// Получени мастера по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор мастера</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MechanicDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long id)
        {
            var mechanic = await _unitOfWork.Mechanics.Get(d => d.Id == id,
                new List<string>() {nameof(Mechanic.CarStation)});
            if (mechanic == null)
                return NotFound();
            return Ok(_mapper.Map<MechanicDto>(mechanic));
        }

        /// <summary>
        /// Создание записи мастера
        /// </summary>
        /// <param name="request">Данные для записи</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] MechanicCreateDto request)
        {
            var mechanic = _mapper.Map<Mechanic>(request);
            var carStation = await _unitOfWork.CarStations.Get(d => d.Id == request.CarStationId,
                new List<string>() {nameof(CarStation.Mechanics)});
            if (carStation.Mechanics.Count > 0 &&
                carStation.Mechanics.Any(d => d.Phonenumber.Equals(mechanic.Phonenumber)))
            {
                return BadRequest("Механик с таким номером телефона уже привязан к сервису");
            }

            mechanic.CarStation = carStation;
            await _unitOfWork.Mechanics.Create(mechanic);
            await _unitOfWork.Save(1);
            return Ok();
        }

        /// <summary>
        /// Обновление записи мастера
        /// </summary>
        /// <param name="id">Идентификатор записи для обновления</param>
        /// <param name="requestDto">Объект с данными</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(long id, [FromBody] MechanicDto requestDto)
        {
            if (id != requestDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mechanic = await _unitOfWork.Mechanics.Get(d => d.Id == requestDto.Id,
                new List<string>() {nameof(Mechanic.CarStation)});
            if (mechanic == null)
            {
                return NotFound();
            }

            if (requestDto.CarStation.Id != mechanic.CarStationId)
            {
                var carStation = await _unitOfWork.CarStations.Get(d => d.Id == requestDto.CarStation.Id);
                mechanic.CarStation = carStation;
            }

            mechanic.Phonenumber = requestDto.Phonenumber;
            mechanic.FirstName = requestDto.FirstName;
            mechanic.LastName = requestDto.LastName;
            await _unitOfWork.Mechanics.Update(mechanic);
            await _unitOfWork.Save(1);
            return Ok();
        }

        /// <summary>
        /// Удаление одной записи
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(long id)
        {
            var mechanic = await _unitOfWork.Mechanics.Get(d => d.Id == id,
                new List<string>() {nameof(Mechanic.CarStation)});
            if (mechanic == null)
            {
                return NotFound();
            }

            _unitOfWork.Mechanics.Delete(mechanic);
            return Ok();
        }
    }
}
