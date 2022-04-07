# PatikaDev-Logo-Net-Bootcamp-Homework-5

* BackgroundWorker oluşturulacak. https://jsonplaceholder.typicode.com/posts bu linketeki her bir dakikada çalışıp bu bilgileri çekip veri tabanına kayıt eden bir repository oluşturulacak.
* https://drive.google.com/file/d/17OUbFAua2kTngQLO7FGY-kxwvXLxnDEZ/view?usp=sharing bunun üstüne oluşturabilirsin. Post diye tablo oluşturulacak migration ile user, id, title, body postun kolonları olacak. 

## Yapılanlar

* Katmanlar oluşturuldu ve db işlemleri için Post repository oluşturuldu.
* Producer ve Consumer adında 2 tane background worker ve verileri görebilmek için API oluşturuldu.
* RabbitMQ kullanılarak Queue oluşturuldu.
* Producer verileri adresten 60 saniyede bir liste olarak çekip daha sonra onları Post sınıfı listesi haline getiriyor. Daha sonra bir post-queue adında bir queue oluşturuluyor ve veriler oraya atılıyor.
* Consumer Producerın attığı verileri alıp database'e insert ediyor.
* Birden fazla Producer ve Consumer aynı anda çalışabilir.
