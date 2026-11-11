# SkyHop

Fırat Üniversitesi Yazılım Mühendisliği 2. sınıf **Nesne Tabanlı Programlama (YMH219)** dersi için hazırlanmış, Flappy Bird benzeri bir Windows Forms oyunudur. Amaç, Space ile kuşu zıplatarak engellerin arasından geçmek ve skor kazanmaktır.

## Kullanılan teknolojiler

- C# ve .NET 10 (`net10.0-windows`)
- Windows Forms; Designer yerine kodla oluşturulan arayüz
- System.Drawing ile basit şekiller
- Git / GitHub: [ugurmetinkarabulut/SkyHop](https://github.com/ugurmetinkarabulut/SkyHop)

`UseWindowsForms` ve `EnableWindowsTargeting` etkindir. Harici grafik, oyun motoru veya ek NuGet paketi kullanılmaz.

## Oyunun temel özellikleri

- SkyHop başlığı, Oyuna Başla butonu ve Space ile başlangıç
- Her Space basışında zıplama, sürekli yer çekimi; basılı tutmak tekrar zıplatmaz
- Yeniden başlatırken Space basılıysa, yeni zıplama için bırakılıp tekrar basılması gerekir
- Sabit hızla sola ilerleyen üç üst/alt engel çifti; başlangıçta ikisi görünür
- Ekrandan çıkan engellerin sağ tarafa taşınması ve boşluk yüksekliğinin rastgele değişmesi
- Tamamen geçilen her engel çifti için yalnızca bir puan
- Engel veya zemin çarpışmasında Timer'ın durması ve Game Over ekranı
- Yeniden Başlat butonuyla skor, kuş, engeller ve puan bayraklarının sıfırlanması
- Açık mavi renk geçişli gökyüzü, yumuşak gölgeli bulutlar ve güneş
- Dış çizgili sarı kuş, gölgeli yeşil borular ve çimenli zemin
- Başlangıç panelinde kısa açıklama ve Space ipucu, Game Over panelinde son skor
- Oyun sırasında sol üstte okunaklı skor kutusu

## Windows üzerinde çalıştırma

Gereksinimler: Windows 10/11, .NET 10 SDK ve .NET 10 destekleyen Visual Studio'nun **.NET masaüstü geliştirme** iş yükü.

1. Projeyi `git clone https://github.com/ugurmetinkarabulut/SkyHop.git` ile indirin veya GitHub'dan ZIP olarak indirip açın.
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
- İkinci aşamada restore ve build yeniden çalıştırıldı; sonuç yine **0 hata, 1 NU1900 uyarısı** oldu.
- Geçici konsol kontrolünde gerçek `Bird.cs` ve `Pipe.cs` ile `Form1` olay metotlarının aynı gövdeleri kullanıldı. Windows Forms kontrolleri yerine basit test nesneleri kullanıldığı için bu bir Windows arayüz testi değildir. Yeniden başlatmada basılı Space sorunu eski kodda yeniden üretildi; düzeltilmiş kodda kontrol geçti.
- Tek zıplama, yer çekimi, üst sınır, geçişten önce puan verilmemesi, tek puan, üst/alt/zemin çarpışması, Game Over'da Timer'ın durdurulması ve yeniden başlatma durumu kontrol edildi. 10.000 döngü adımında 137 engel yeniden yerleştirildi; engel aralıkları ve boşluk sınırları korundu. Döngü içindeki kontrollerle birlikte 60.021 doğrulama geçti. Engel döngüsü kontrolünde kuş, çarpışmaları önlemek için oyun alanından uzak tutuldu.
- Geçici kontrol projesinde nullable bağlamı (`CS8632`) ve sahte odak alanının okunmaması (`CS0414`) uyarıları görüldü; bunlar SkyHop derlemesine ait değildir. Kontrol dosyaları proje yapısını büyütmemek için depoya eklenmedi.

12 Kasım 2026'da görsel değişikliklerden sonra `dotnet build SkyHop.sln` macOS üzerinde başarıyla tamamlandı. Derleme 0 hata verdi. NuGet güvenlik verilerine erişilemediği için aynı `NU1900` uyarısı geri yükleme ve derleme sırasında iki kez raporlandı.

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

Yerel depo, `origin` üzerinden [ugurmetinkarabulut/SkyHop](https://github.com/ugurmetinkarabulut/SkyHop) repository'sine bağlıdır. Ana dal `main` olup `origin/main` dalını takip eder. İlk üç geliştirme commiti GitHub'a gönderilmiştir. `bin/`, `obj/` ve `.vs/` sürüm kontrolüne alınmaz.
