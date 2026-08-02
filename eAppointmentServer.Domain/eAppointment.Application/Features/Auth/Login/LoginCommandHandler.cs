using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Feature.Auth.Login;

// LoginCommandHandler handlerını başka sınıfların bilmesine gerek yok. Bu yüzden bu classı internal yapıyoruz.


//CTRL + . ile IRequestHandler interface'ini implement ediyoruz. Bu interface iki generic parametre alıyor. Birincisi request tipi, ikincisi response tipi.
internal sealed class LoginCommandHandler (
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider): IRequestHandler<LoginCommand, Result<LoginCommandResponse>> 
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Burada kullanıcıyı buluyoruz. Kullanıcı adı veya email ile arama yapıyoruz.Request'ten gelen UserNameOrEmail parametresini kullanıyoruz.
        AppUser? appUser = await userManager.Users.FirstOrDefaultAsync(p =>
        p.UserName == request.UserNameOrEmail ||
        p.Email == request.UserNameOrEmail, cancellationToken); // Burada kullanıcıyı bulamazsak null dönecek. Bu yüzden nullable
        if(appUser is null)
        {
            return Result<LoginCommandResponse>.Failure("User not found"); // Eğer kullanıcı bulunamazsa, Result tipinde bir hata mesajı döndürüyoruz.
        }
        bool isPasswordCorrect = await userManager.CheckPasswordAsync(appUser,request.Password); // Burada kullanıcı şifresini kontrol ediyoruz. Eğer şifre yanlışsa false dönecek.)
        if(!isPasswordCorrect)
        {
            return Result<LoginCommandResponse>.Failure("Incorrect password"); // Eğer şifre yanlışsa, Result tipinde bir hata mesajı döndürüyoruz.
        }
        string token = await jwtProvider.CreateTokenAsync(appUser); // Eğer kullanıcı bulundu ve şifre doğruysa, token oluşturuyoruz. Burada IJwtProvider interface'ini kullanıyoruz.
                                                         // Bu interface'i implement eden bir class olacak ve bu class'ta token oluşturma işlemi yapılacak.
        LoginCommandResponse response = new(token); // Burada token'ı response'a atıyoruz. Bu response daha sonra Result tipinde döndürülecek.
        return Result<LoginCommandResponse>.Succeed(response); // Eğer kullanıcı bulundu ve şifre doğruysa,
        // Result tipinde bir başarı mesajı döndürüyoruz. Burada token'ı döndürüyoruz. Token'ı daha sonra oluşturacağız.

    }

}  

