# Stock Management – Stok Yönetim Sistemi

Bu proje, ürün, müşteri, kullanıcı, fatura ve stok işlemlerinin tek bir sistem üzerinden yönetilmesini sağlayan full stack bir stok yönetim uygulamasıdır.

Projenin amacı; gerçek bir stok yönetim senaryosunu Clean Architecture yapısı içerisinde geliştirmek, Angular ile kullanıcı arayüzü oluşturmak, .NET Web API ile iş kurallarını yönetmek ve rol bazlı güvenli bir sistem oluşturmaktır.

## Projede Bulunan Özellikler

- Kullanıcı giriş sistemi
- JWT ile kimlik doğrulama
- Rol bazlı yetkilendirme
- Kullanıcı kayıt sistemi
- Kullanıcı ekleme, güncelleme ve silme
- Ürün ekleme, güncelleme ve silme
- Ürün adına ve barkoda göre arama
- Barkod formatı ve benzersizlik kontrolü
- Müşteri ekleme, güncelleme ve silme
- Alış ve satış faturası oluşturma
- Fatura güncelleme ve silme
- Fatura detaylarına birden fazla ürün ekleme
- Alış faturasında otomatik stok artırma
- Satış faturasında otomatik stok azaltma
- Satış işlemlerinde stok yeterlilik kontrolü
- Fatura güncelleme işleminde eski stok etkisini geri alma
- Fatura silme işleminde stok hareketini geri çevirme
- Kullanıcı rolündeki müşterinin yalnızca kendi faturalarını görmesi
- Faturaları tarih ve tutara göre sıralama
- Finansal özet raporlarının görüntülenmesi
- Form doğrulama işlemleri
- Başarılı ve hatalı işlemler için bildirim mesajları
- Swagger üzerinden API endpointlerini test etme

## Kullanılan Teknolojiler

### Backend

- .NET 10 Web API
- C#
- Clean Architecture
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- MediatR
- CQRS
- AutoMapper
- Generic Repository
- Unit of Work
- TS.Result
- Swagger
- Newtonsoft.Json

### Frontend

- Angular 22
- TypeScript
- HTML
- CSS
- PrimeNG
- Reactive Forms
- RxJS
- JWT Decode
- HTTP Interceptor
- Auth Guard
- Role Guard

## Proje Yapısı

```text
StockManagementARCA
│
├── StockManagementARCA
│   └── Angular frontend projesi
│
├── StockManagementServer.Domain
│   └── Entity, enum ve repository arayüzleri
│
├── StockManagementServer.Application
│   └── Command, Query, Handler, Response ve iş kuralları
│
├── StockManagementServer.Infrastructure
│   └── Veritabanı, repository ve servis implementasyonları
│
└── StockManagementWebAPI
    └── Controller, endpoint ve uygulama ayarları
