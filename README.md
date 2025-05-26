# FluentEmail_ConsoleApp

این پروژه شامل یک اپلیکیشن کنسول دات‌نت (Console) است که با استفاده از کتابخانه FluentEmail امکان ارسال ایمیل را فراهم می‌کند.

---

## ویژگی‌ها

- ارسال ایمیل با استفاده از SMTP و FluentEmail
- استفاده از رندر Razor برای قالب‌بندی ایمیل
- پیاده‌سازی ساده و قابل توسعه

---

## پیش‌نیازها

- .NET 8.0 یا بالاتر
- دسترسی به یک سرور SMTP (مثلاً Gmail)

> **نکته امنیتی:**
> برای ارسال ایمیل از طریق Gmail، به‌جای رمز عبور اصلی، باید از "App Password" استفاده کنید (در صورت فعال بودن احراز هویت دومرحله‌ای).

### ساخت App Password برای Gmail

1. وارد حساب Google خود شوید: [Google Account Security](https://myaccount.google.com/security)
2. مطمئن شوید 2-Step Verification فعال است.
3. به بخش [App Passwords](https://myaccount.google.com/apppasswords) بروید.
4. از منوی کشویی، گزینه‌ی **Mail** و دستگاه موردنظر را انتخاب کنید.
5. روی **Generate** بزنید و رمز ۱۶ رقمی را کپی کنید.
6. این رمز را به جای رمز اصلی Gmail در برنامه وارد کنید.

---

## نحوه اجرا (کنسول)

۱. اطلاعات SMTP و ایمیل را در فایل `Program.cs` تنظیم کنید:

```csharp
// تنظیمات SMTP
var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
{
    EnableSsl = true,
    DeliveryMethod = SmtpDeliveryMethod.Network,
    Port = 587,
    Credentials = new System.Net.NetworkCredential("sender@gmail.com", "app_password")
});

// مقداردهی اولیه FluentEmail
Email.DefaultSender = sender;
Email.DefaultRenderer = new RazorRenderer();

// ارسال ایمیل
var email = await Email
    .From("sender@gmail.com")
    .To("reciver@gmail.com", "Recipient Name")
    .Subject("hello from fluentsmtp")
    .Body("send message abolfazlshabani as messager")
    .SendAsync();

Console.WriteLine(email.Successful
    ? "send message"
    : $"has error: {string.Join(", ", email.ErrorMessages)}");
```

۲. پروژه را با دستور زیر اجرا کنید:

```powershell
# اجرای برنامه
cd FluentEmail_ConsoleApp
 dotnet run
```

---

## پروژه MVC

در حال حاضر این پروژه فقط شامل نسخه کنسول است. برای راه‌اندازی پروژه MVC با همین ساختار:

۱. یک پروژه ASP.NET Core MVC جدید ایجاد کنید.
۲. پکیج‌های `FluentEmail.Core`، `FluentEmail.Razor` و `FluentEmail.Smtp` را نصب کنید.
۳. تنظیمات SMTP و ارسال ایمیل را مشابه پروژه کنسول در کنترلرها پیاده‌سازی کنید.

### نمونه کد راه‌اندازی در MVC

```csharp
// Startup.cs یا Program.cs
builder.Services
    .AddFluentEmail("sender@gmail.com") // ایمیل فرستنده
    .AddRazorRenderer() // استفاده از Razor برای قالب
    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
    {
        Credentials = new System.Net.NetworkCredential("sender@gmail.com", "app_password"),
        EnableSsl = true,
        Port = 587,
    });
```

### نمونه کد ارسال پیام در MVC

```csharp
private readonly IFluentEmail _fluentEmail;

public HomeController(IFluentEmail fluentEmail)
{
    _fluentEmail = fluentEmail;
}

public async Task<IActionResult> Index()
{
    var result = await _fluentEmail
        .To("recipient@example.com", "Recipient")
        .Subject("testMessage")
        .Body("hello this is a test message")
        .SendAsync();

    Console.WriteLine(result.Successful
        ? "send message"
        : $"has error: {string.Join(", ", result.ErrorMessages)}");
    return View();
}
```

---

## منابع

- [FluentEmail Documentation](https://github.com/lukencode/FluentEmail)
- [آموزش ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core/mvc/)

---

> توسعه‌دهنده: ابوالفضل شعبانی
