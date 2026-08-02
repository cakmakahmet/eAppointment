namespace eAppointmentServer.Application.Feature.Auth.Login;

public sealed record LoginCommandResponse(
    string Token);   //Bunlar tamamlandıktan sonra geriye bir token döndürecek. Bu iki class dependency injection ile handler classına ve handlera metotuna ulaşacaklar ancak 

