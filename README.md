# eAppointment – Randevu Yönetim Sistemi

Bu proje, **Angular ve .NET 8 ile Full Stack Web Geliştirme kursu** kapsamında geliştirilmiş bir hastane randevu yönetim uygulamasıdır.

Projenin amacı; Angular ile frontend geliştirme, .NET Web API oluşturma, veritabanı işlemleri, JWT ile kullanıcı girişi, yetkilendirme ve CRUD işlemleri gibi temel full stack konularını uygulamalı olarak öğrenmektir.

## Projede Bulunan Özellikler

- Kullanıcı giriş sistemi
- JWT ile kimlik doğrulama
- Rol bazlı yetkilendirme
- Kullanıcı ekleme, güncelleme ve silme
- Doktor ekleme, güncelleme ve silme
- Hasta ekleme, güncelleme ve silme
- Randevu oluşturma
- Randevu güncelleme ve silme
- Doktorların bölüme göre listelenmesi
- Doktora ait randevuların takvim üzerinde gösterilmesi
- Form doğrulama işlemleri
- Başarılı ve hatalı işlemler için bildirim mesajları
- Swagger üzerinden API endpointlerini test etme

## Kullanılan Teknolojiler

### Backend

- .NET 8 Web API
- C#
- Entity Framework Core
- SQL Server LocalDB
- ASP.NET Core Identity
- JWT Authentication
- MediatR
- CQRS
- AutoMapper
- FluentValidation
- TS.Result
- Swagger

### Frontend

- Angular
- TypeScript
- HTML
- CSS
- Bootstrap
- DevExtreme
- SweetAlert2
- RxJS
- JWT Decode

## Proje Yapısı

```text
eAppointment
│
├── eAppointmentClient
│   └── Angular frontend projesi
│
└── eAppointmentServer.Domain
    ├── eAppointmentServer.Domain
    ├── eAppointment.Application
    ├── eAppointmenServer.Infrastructure
    └── eAppointmentServer.WebAPI
```

## Backend Katmanları

### Domain

Entity sınıfları, enumlar ve repository arayüzleri bu katmanda bulunmaktadır.

### Application

Command, Query, Handler, Mapping, Validation ve servis arayüzleri bu katmanda bulunmaktadır.

### Infrastructure

Veritabanı bağlantısı, repository sınıfları, migration dosyaları ve JWT servisi bu katmanda bulunmaktadır.

### WebAPI

Controller sınıfları, uygulama ayarları ve API başlangıç yapılandırmaları bu katmanda bulunmaktadır.

## Projeyi Çalıştırma

Projeyi çalıştırmadan önce bilgisayarınızda aşağıdaki araçların kurulu olması gerekir:

- .NET 8 SDK
- Node.js
- npm
- Angular CLI
- SQL Server veya SQL Server LocalDB

## Backend Projesini Çalıştırma

Terminal üzerinden Web API klasörüne geçin:

```bash
cd eAppointmentServer.Domain/eAppointmentServer.WebAPI
```

Gerekli paketleri yükleyin:

```bash
dotnet restore
```

Projeyi çalıştırın:

```bash
dotnet run
```

API varsayılan olarak aşağıdaki adres üzerinden çalışmaktadır:

```text
https://localhost:7168
```

Swagger sayfası:

```text
https://localhost:7168/swagger
```

## Frontend Projesini Çalıştırma

Yeni bir terminal açarak Angular projesine geçin:

```bash
cd eAppointmentClient
```

Gerekli paketleri yükleyin:

```bash
npm install
```

Angular projesini çalıştırın:

```bash
npm start
```

Alternatif olarak:

```bash
ng serve
```

Tarayıcı üzerinden aşağıdaki adrese gidin:

```text
http://localhost:4200
```

## Veritabanı

Proje, SQL Server LocalDB kullanmaktadır.

Veritabanı bağlantı ayarları aşağıdaki dosyada bulunmaktadır:

```text
eAppointmentServer.Domain/eAppointmentServer.WebAPI/appsettings.json
```

Migration işlemlerinden sonra veritabanı Entity Framework Core tarafından oluşturulmaktadır.

## Projede Öğrenilen Konular

Bu proje geliştirilirken aşağıdaki konular üzerinde çalışılmıştır:

- Katmanlı mimari
- RESTful API geliştirme
- Angular ve Web API bağlantısı
- Dependency Injection
- Repository Pattern
- CQRS ve MediatR
- Entity Framework Core
- Migration işlemleri
- JWT oluşturma ve kullanma
- Authentication ve Authorization
- Angular Forms
- Form validation
- HTTP servisleri
- Angular component yapısı
- Pipe kullanımı
- Takvim üzerinde randevu yönetimi
- Hata yönetimi
- Bildirim sistemleri

## Projenin Amacı

Bu proje ticari kullanım amacıyla değil, eğitim ve kişisel gelişim amacıyla hazırlanmıştır.

Angular ve .NET 8 kullanılarak uçtan uca bir full stack uygulamanın nasıl geliştirildiğini öğrenmek için oluşturulmuştur.

## Geliştirici

**Ahmet Melih Çakmak**
