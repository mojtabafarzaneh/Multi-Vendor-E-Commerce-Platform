using Microsoft.AspNetCore.Identity;
using System;

class Program
{
    static void Main()
    {
        var passwordHasher = new PasswordHasher<IdentityUser>();
        var hashedPassword = passwordHasher.HashPassword(null, "Mojtaba7878*");
        Console.WriteLine(hashedPassword);
    }
}