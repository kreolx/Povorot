using System.Collections.Generic;
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
    public class WorkCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkCategoryController> _logger;

        public WorkCategoryController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WorkCategoryController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает список доступных категорий работ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<WorkCategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List()
        {
            var workCategories = await _unitOfWork.WorkCategories.List();
            return Ok(_mapper.Map<List<WorkCategoryDto>>(workCategories));
        }

        /// <summary>
        /// Возвращает запись по идентификатору
        /// </summary>
        /// <param name="id">Иденитификатор записи</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkCategoryDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long id)
        {
            var workCategory = await _unitOfWork.WorkCategories.Get(d => d.Id == id);
            if (workCategory == null)
                return NotFound();

            return Ok(_mapper.Map<WorkCategoryDto>(workCategory));
        }

        /// <summary>
        /// Создает категорию работ
        /// </summary>
        /// <param name="request">Объект с данными для создания</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(WorkCategoryDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] WorkCategoryCreateDto request)
        {
            var workCategory = _mapper.Map<WorkCategory>(request);
            await _unitOfWork.WorkCategories.Create(workCategory);
            await _unitOfWork.Save(1);
            return CreatedAtAction(nameof(Get), new {Id = workCategory.Id}, _mapper.Map<WorkCategoryDto>(workCategory));
        }

        /// <summary>
        /// Обновляет поля записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <param name="requestDto">Объект с полями для обновления</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(long id, [FromBody] WorkCategoryDto requestDto)
        {
            if (id != requestDto.Id)
            {
                return BadRequest();
            }

            var existWorkCategory = await _unitOfWork.WorkCategories.Get(d => d.Id == id);
            if (existWorkCategory == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existWorkCategory.Name = requestDto.Name;
                existWorkCategory.Description = requestDto.Description;
                _unitOfWork.WorkCategories.Update(existWorkCategory);
                await _unitOfWork.Save(1);
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Удаление записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(long id)
        {
            var existWorkCategory = await _unitOfWork.WorkCategories.Get(d => d.Id == id);
            if (existWorkCategory == null)
            {
                return NotFound();
            }

            _unitOfWork.WorkCategories.Delete(existWorkCategory);
            return Ok();
        }
    }
}
