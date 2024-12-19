HotelBook Project
Projede Clean Architecture system design yaklaşımı kullanılmıştır.
Proje Katmanları
  - Domain Layer
   Domain business kurallarını içermektedir.
  - Application Layer
   Application business rulelar bulunmaktar.
   CQRS ile command ve queryler ayrılmıştır. Meditor Design Pattern, MeditR kütüphanesi ile birlikte uygulanmıştır.
   DTOları için Automapper kütüphanesi kullanılmıştır.
   CustomException, Logging Middleware, Validation classları burda yer almaktadır. Validation için FluentValidation kullanılmıştır.
   Logging için Serilog ve ElasticSearch kullanılmıştır.
  - Infrastructure Layer
   Database Context ve Repositoryler yer almaktadır.
   Bu katmanda EntityTypeConfigrationlar belirlenerek, Migration ile DB tabloları ve değişiklikler uygulanmıştır. Code First yaklaşımı uygulanmıştır.
   Repository ve UnitOfWork Patternler uygulanmıştır.
  - Presentation Layer
    User interactions için API projemiz yer almaktadır.
Projede unit testler için xUnit kullanılmıştır.
Ayrıca Moq ve FluentAssertion kütüphaneleri kullanılmıştır.

Report Project
Report API
Kullanıcının rapor taleplerini alıp RabbitMQ ile Console Appe ileten ve rapor listelerini kullanıcıya sunan API projesdir.
Report Console
İletilen raporları, ilgili RabbitMQ channelını dinleyerek alan ve rapor oluşturup, DB'de rapor statüsünü güncelleyen Console projesidir.
