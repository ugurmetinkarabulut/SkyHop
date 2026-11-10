# SkyHop

Fırat Üniversitesi Yazılım Mühendisliği 2. sınıf **Nesne Tabanlı Programlama (YMH219)** dersi için hazırlanmış, Flappy Bird benzeri bir Windows Forms oyunudur. Amaç, Space ile kuşu zıplatarak engellerin arasından geçmek ve skor kazanmaktır.

## Kullanılan teknolojiler

- C# ve .NET 10 (`net10.0-windows`)
- Windows Forms; Designer yerine kodla oluşturulan arayüz
- System.Drawing ile basit şekiller
- Yerel Git deposu; planlanan GitHub repository adı: **SkyHop**

`UseWindowsForms` ve `EnableWindowsTargeting` etkindir. Harici grafik, oyun motoru veya ek NuGet paketi kullanılmaz.

## Oyunun temel özellikleri

- SkyHop başlığı, Oyuna Başla butonu ve Space ile başlangıç
- Her Space basışında zıplama, sürekli yer çekimi; basılı tutmak tekrar zıplatmaz
- Sabit hızla sola ilerleyen üç üst/alt engel çifti; başlangıçta ikisi görünür
- Ekrandan çıkan engellerin sağ tarafa taşınması ve boşluk yüksekliğinin rastgele değişmesi
- Tamamen geçilen her engel çifti için yalnızca bir puan
- Engel veya zemin çarpışmasında Timer'ın durması ve Game Over ekranı
- Yeniden Başlat butonuyla skor, kuş, engeller ve puan bayraklarının sıfırlanması
- Açık mavi gökyüzü, sarı kuş, yeşil engeller ve zemin

## Windows üzerinde çalıştırma

Gereksinimler: Windows 10/11, .NET 10 SDK ve .NET 10 destekleyen Visual Studio'nun **.NET masaüstü geliştirme** iş yükü.

1. Proje klasörünü Windows bilgisayarına kopyalayın. Uzak GitHub deposu henüz oluşturulmadı.
2. Visual Studio ile `SkyHop.sln` dosyasını açın.
3. Başlangıç projesinin SkyHop olduğundan emin olup **F5** tuşuna basın.
4. **Oyuna Başla** butonuna tıklayın veya **Space** tuşuna basın.
5. **Space** ile zıplayın. Oyun bitince **Yeniden Başlat** butonunu kullanın.

Alternatif olarak Windows terminalinde proje klasöründen:

```powershell
dotnet restore SkyHop.sln
dotnet build SkyHop.sln
dotnet run --project SkyHop.csproj
```

## Proje dosyaları

- `Form1.cs`: arayüz, Timer, klavye olayları, çizim, çarpışma, skor ve oyun durumu
- `Bird.cs`: kuşun konumu, hızı, zıplaması, yer çekimi ve sıfırlanması
- `Pipe.cs`: üst/alt engel dikdörtgenleri, hareket ve puan bayrağı
- `Program.cs`: Windows Forms uygulamasının giriş noktası
- `KOD_ACIKLAMALARI.md`: derste anlatmak için kısa kod açıklamaları
- `TEST_CHECKLIST.md`: Windows üzerinde uygulanacak manuel kontroller

Üç temel sınıf kullanılır. `Program` yalnızca uygulamayı başlatır; GameEngine, servis veya ayrı Core projesi yoktur.

## Derleme durumu

10 Kasım 2026 tarihinde macOS üzerinde .NET SDK **10.0.401** ile:

- `dotnet restore SkyHop.sln`: tamamlandı.
- `dotnet build SkyHop.sln --no-restore`: başarılı, **0 hata ve 1 uyarı**.
- Uyarı `NU1900`: ortamdan NuGet güvenlik verilerine erişilemedi. Güvenlik denetimi kapatılmadı.
- Gerçek `Bird.cs` ve `Pipe.cs` dosyalarıyla geçici bir konsol programında yapılan 10 mantık kontrolü geçti: yer çekimi, zıplama, üst sınır, kuş sıfırlama, boşluktan geçiş, üst/alt engel kesişimi, engel hareketi, engel sıfırlama ve boşluk/zemin geometrisi. Kontrol programı proje yapısını büyütmemek için depoya eklenmedi.

Bu sonuç, Windows'ta çalıştırma veya görsel doğrulama anlamına gelmez. Windows Forms macOS üzerinde çalışmaz. Windows oynanış testleri henüz yapılmadı.

## Gerçek ekran görüntüleri

Görseller henüz oluşturulmadı. Windows'ta uygulamayı çalıştırınca en az iki gerçek ekran görüntüsü ekleyin:

### 1. Başlangıç ekranı

Eklenecek dosya: `screenshots/01-oyun-baslangic.png` — SkyHop başlığı ve Oyuna Başla butonu.

### 2. Oyun sırasında

Eklenecek dosya: `screenshots/02-oyun-devam.png` — kuş, engeller ve kazanılmış skor.

Dosyalar eklendikten sonra README'ye Markdown görsel bağlantıları ekleyin. Sahte görüntü veya çalışmayan görsel bağlantısı eklenmemiştir.

## Bilinen sınırlamalar

- Windows üzerinde açılış, çizim, klavye odağı ve oynanış henüz doğrulanmadı; `TEST_CHECKLIST.md` tamamlanmalı.
- Pencere sabit 800 × 650 istemci alanına sahiptir. Windows ekran ölçeklendirmesinde görünüm ayrıca kontrol edilmelidir.
- Hareket Timer adımlarına bağlıdır; yoğun sistemlerde oyun yavaşlayabilir.
- Çarpışma kutusu dikdörtgendir; kuşun oval çiziminin köşelerinde de çarpışma sayılabilir.
- Kuş üst sınıra gelince yukarı çıkması engellenir; oyun bitmez.
- Ses, duraklatma ve kalıcı en yüksek skor bulunmaz. Pencere odağını kaybedince oyun devam eder.

## Öğrenci bilgileri

- Ad Soyad: Uğur Metin Karabulut
- Öğrenci Numarası: 250541103
- Ders: YMH219 — Nesne Tabanlı Programlama

## GitHub durumu

Yerel Git deposu oluşturulmuştur. `bin/`, `obj/` ve `.vs/` sürüm kontrolüne alınmaz. Uzak repository oluşturma, bağlantı kurma ve push işlemleri sonraki talimat beklenerek yapılacaktır. GitHub repository adı **SkyHop** olmalıdır.
