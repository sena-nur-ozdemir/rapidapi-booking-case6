# 💎 RapidAPI Integration Project

Dinamik otel arama, otel detayları ve gerçek zamanlı dashboard verilerini RapidAPI üzerinden çeken "Dark Luxury" temalı modern bir ASP.NET Core web uygulaması.

## Proje Hakkında

Bu proje, M&Y Yazılım Eğitim Akademi .NET Full Stack Bootcamp kapsamında geliştirilen **Case #6** çalışmasıdır. 

RapidAPI platformu üzerinden çekilen tamamen dinamik verilerle çalışan, ASP.NET Core MVC ile geliştirilmiş bir otel arama ve listeleme uygulamasıdır. Projede **veritabanı veya admin paneli kullanılmamaktadır**; tüm içerikler ilgili API uç noktalarından (endpoint) asenkron olarak ve anlık getirilmektedir.

🤖 **Yapay Zeka Entegrasyonu:** Projenin lüks arayüz tasarımı (UI/UX) ve kodlama süreçlerinde **Gemini AI** asistanından aktif olarak faydalanılmıştır.

## 🎯 Projenin Amacı ve Mimari

* **Veritabansız Mimari:** Veriler kaydedilmez, anlık JSON formatından DTO'lara (Data Transfer Object) dönüştürülüp ekrana basılır.
* **Çoklu API Tüketimi (Consume):** `HttpClient` ve asenkron programlama (`Task.WhenAll`) kullanılarak farklı API'ler aynı anda projenin içine entegre edilmiştir.
* **Dil ve Para Birimi:** Tüm arama sonuçları global standartlara uygun olarak İngilizce (`en-us`) ve Euro (`EUR`) para birimi üzerinden çekilmektedir.

---

## 🛠 Kullanılan Teknolojiler

* **Backend:** ASP.NET Core 8.0 MVC, C#, IHttpClientFactory, Newtonsoft.Json
* **Mimari:** ViewComponents, Task Asynchronous Programming
* **Veri Kaynağı:** RapidAPI (Booking API ve 6 farklı Global API)
* **Yapay Zeka:** Gemini AI (UI/UX konsept tasarımı)
* **Frontend:** Razor Views, Tailwind CSS, Lucide Icons, Custom CSS

---

## 📄 Sayfa Yapısı ve Akış

### 1. Ana Sayfa ve Otel Listeleme 
Kullanıcıdan *Şehir Adı, Check-in, Check-out ve Kişi Sayısı* bilgileri alınır.
* **Destination ID Alma:** Girilen şehir adı ile (örn: London) `Search Destination API`'ye istek atılır ve ilk sonucun ID'si alınır.
* **Otel Listeleme:** Elde edilen Destination ID kullanılarak `Search Hotels API` üzerinden arama yapılır.
* **Çıktı:** Minimum 20 adet otel; kapak fotoğrafı, puanı (rating), temel özellikleri ve fiyatıyla birlikte koyu temalı lüks kartlarda listelenir. *(API kotasının dolması ihtimaline karşı sistemin çökmemesi için Fallback mimarisi eklenmiştir).*

### 2. Otel Detay Sayfası 
Listeden seçilen otelin `hotel_id` değeri alınarak `Hotel Detail API` uç noktasına istek atılır.
* Asenkron isteklerle otele ait; genel açıklamalar, dinamik olarak hesaplanan puanlar, 2-3 adet fotoğraf, otel olanakları (Facilities) ve müsait odalar (Room List) ekrana basılır.
* Seçilen otelin enlem ve boylam (`Latitude/Longitude`) verilerine göre dinamik **Google Maps** entegrasyonu mevcuttur.

### 3. Dashboard
Ana giriş sayfasıdır. Kullanıcıya günlük hayatta ihtiyaç duyabileceği çeşitli verileri `ViewComponent` mimarisi ile birbirini beklemeden anlık olarak sunar.

| Bölüm | Kullanılan API | Açıklama |
| :--- | :--- | :--- |
| 🌤️ **Hava Durumu** | WeatherAPI.com | Anlık sıcaklık ve hava durumuna uygun ikon |
| 💱 **Döviz Kurları** | Exchange Rates API | EUR, USD ve GBP kurlarının TRY karşılığı |
| 🪙 **Kripto Para** | Crypto Market Prices | BTC, ETH ve SOL anlık USD değerleri |
| ⛽ **Yakıt Fiyatları** | Fuel / Gas Price API | Güncel Benzin, Dizel ve Premium yakıt fiyatları |
| 📰 **Haberler** | Real-Time News Data | Yabancı kaynaklardan güncel global haberler |
| ⚽ **Spor / Canlı Skor** | Free API Live Football Data | Güncel futbol karşılaşmaları ve maç skorları |
| 🛫 **Hızlı Keşif** | Dahili Yönlendirme | Kullanıcıyı doğrudan otel arama modülüne aktaran etkileşimli geçiş kartı |

---

## 📸 Ekran Görüntüleri

Proje arayüzlerini detaylı incelemek için aşağıdaki görsellere göz atabilirsiniz:

### 🌍 Ana Sayfa Arama Paneli 
<img width="1917" height="867" alt="rapidapi2" src="https://github.com/user-attachments/assets/ed7c93ac-8263-41b6-9c5e-2789f524465c" />

### 🏨 Otel Listeleme Paneli
<img width="1920" height="3214" alt="screencapture-localhost-7094-Hotel-HotelList-2026-06-29-02_28_30" src="https://github.com/user-attachments/assets/5dab6fa8-7ee9-432f-ac9a-55d8885db116" />

### 💎 Otel Detayı 
<img width="1920" height="3225" alt="screencapture-localhost-7094-Hotel-HotelDetail-2026-06-29-02_27_37" src="https://github.com/user-attachments/assets/9de9558c-e196-4084-a975-e28ecc826be4" />

### 📊 Gerçek Zamanlı Dashboard
<img width="1920" height="1284" alt="screencapture-localhost-7094-Dashboard-Index-2026-06-29-02_49_32" src="https://github.com/user-attachments/assets/38f25dd3-0ada-4478-83ed-d0f0e84b46c6" />


---

**👩‍💻 Developer:** Sena Nur Özdemir — [GitHub Profilim](https://github.com/sena-nur-ozdemir)
