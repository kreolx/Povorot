using System;
using System.Collections.Generic;
using Povorot.DAL.Models;

namespace Povorot.Api.Dto
{
    public record CarStationDto: CarStationCreateDto
    {
        public long Id { get; set; }
    }

    public record CarStationCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime StartWorkTime { get; set; }
        public DateTime EndWorkTime { get; set; }
        public ICollection<RepairPostDto> RepairPosts { get; set; }
    }
}