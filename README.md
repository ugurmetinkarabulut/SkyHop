# SkyHop

Fırat Üniversitesi Yazılım Mühendisliği 2. sınıf **Nesne Tabanlı Programlama (YMH219)** dersi için geliştirilen Flappy Bird benzeri Windows Forms oyunu.

[GitHub repository](https://github.com/ugurmetinkarabulut/SkyHop)

## Teknolojiler ve yapı

C#, .NET 10 (`net10.0-windows`), Windows Forms ve System.Drawing kullanılır. `UseWindowsForms` ve `EnableWindowsTargeting` etkindir. Arayüz kodla oluşturulur.

- `Form1.cs` oyun döngüsünü, klavyeyi, skoru, çarpışmayı ve çizimleri yönetir.
- `Bird.cs` kuşun konumunu, zıplamasını ve yer çekimini yönetir.
- `Pipe.cs` üst/alt engel çiftini, hareketini ve puan bayrağını tutar.
- `Program.cs` uygulamayı başlatır.
- `KOD_ACIKLAMALARI.md` derste anlatılabilecek kısa açıklamaları içerir.
- `TEST_CHECKLIST.md` Windows üzerinde uygulanacak kontrolleri içerir.

## Özellikler ve kontroller

- **Oyuna Başla** veya ilk ekranda **Space** ile başlangıç.
- Her Space basışında tek zıplama ve sürekli yer çekimi. Yeniden zıplamak için tuş bırakılmalıdır.
- Üç hareketli engel çifti, değişen boşluklar ve tamamen geçilen her çiftten tek puan.
- Engel veya zemin çarpışmasında Game Over ve Timer'ın durması.
- **Yeniden Başlat** ile skor, hız ve nesne konumlarının sıfırlanması. Basılı Space bırakılana kadar tekrar zıplatmaz.
- Mavi gökyüzü, bulutlar, güneş, sarı kuş, yeşil borular, çimenli zemin ve okunaklı skor.

## Windows üzerinde çalıştırma

Windows 10/11, .NET 10 SDK ve .NET 10 destekleyen Visual Studio'nun **.NET masaüstü geliştirme** iş yükü gerekir.

1. Repository'yi klonlayın veya GitHub'dan ZIP olarak indirin.
2. Visual Studio'da `SkyHop.sln` dosyasını açıp **F5** tuşuna basın.
3. Oyunu başlatın ve **Space** ile engellerin arasından geçin.

Windows terminalinde proje klasöründen de çalıştırabilirsiniz.

```powershell
dotnet restore SkyHop.sln
dotnet build SkyHop.sln
dotnet run --project SkyHop.csproj
```

## Doğrulama durumu

18 Kasım 2026'da macOS ve .NET SDK 10.0.401 üzerinde restore tamamlandı. `dotnet build SkyHop.sln --no-restore` **0 hata, 1 NU1900 uyarısı** ile başarılı oldu. Uyarı, NuGet güvenlik verilerine erişilememesinden kaynaklanır. Güvenlik denetimi açıktır.

Space denetiminde, Microsoft'un `SuppressKeyPress` için belirttiği `KeyUp` bastırma riski giderildi. Space karakteri `KeyPress` içinde `Handled` ile işlenir, bırakma durumu `KeyUp` içinde temizlenir. [Microsoft klavye olayları belgesi](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/input-keyboard/events)

Geçici konsol kontrolünde gerçek `Bird` ve `Pipe` sınıfları ile `Form1` olay metotlarının gövdeleri kullanıldı. 100 Space bırakıp basma dizisi, basılı tuş tekrarı, yeniden başlatma, yer çekimi, skor ve çarpışma kontrolleri geçti. 2.000 adımda 26 engel yenilemesinde aralıklar ve boşluk sınırları korundu. Form, Timer ve klavye olay verileri basit test nesneleriyle temsil edildi.

Windows'ta açılış, klavye odağı, oynanış ve görsel testler bekliyor. Windows Forms macOS üzerinde çalıştırılamaz.

## Bilinen sınırlamalar

- Oyun alanı sabit 800 × 650 pikseldir. Windows %100 ve %150 ölçeklendirmede kontrol edilmelidir.
- Hareket Timer adımlarına bağlıdır. Yoğun sistemlerde yavaşlayabilir.
- Kuşun çarpışma kutusu dikdörtgendir. Üst sınır kuşu durdurur, oyun bitmez.
- Pencere odağını kaybedince oyun devam eder. Ses, duraklatma ve kalıcı yüksek skor bulunmaz.

## Gerçek ekran görüntüleri

Windows'ta uygulama çalıştırılınca aşağıdaki iki görüntü alınarak buraya eklenecek.

1. **Başlangıç ekranı** — `screenshots/01-oyun-baslangic.png`
2. **Oyun sırasında kuş, engeller ve skor** — `screenshots/02-oyun-devam.png`

## Öğrenci bilgileri

- Ad Soyad — Uğur Metin Karabulut
- Öğrenci Numarası — 250541103
- Ders — YMH219, Nesne Tabanlı Programlama

Yerel `main`, `origin/main` dalını takip eder. `bin/`, `obj/` ve `.vs/` sürüm kontrolü dışında tutulur.
