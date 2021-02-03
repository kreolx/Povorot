namespace Povorot.Api.Dto
{
    public record UserDto: UserCreateDto
    {
        public long Id { get; set; }
    }

    public record UserCreateDto
    {
        public string UserName { get; set; }
    }
}