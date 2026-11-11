# Windows manuel test listesi

Bu kontroller henüz yapılmadı. macOS üzerinde restore ve derleme başarılıdır; Windows çalıştırma testi yerine geçmez.

- [ ] .NET 10 SDK ve Visual Studio .NET masaüstü geliştirme iş yükü kurulu.
- [ ] `SkyHop.sln` Visual Studio'da açılıyor ve F5 ile çalışıyor.
- [ ] Başlangıçta SkyHop başlığı, Oyuna Başla butonu ve iki görünür engel çifti var; nesneler beklerken hareket etmiyor.
- [ ] Oyuna Başla butonu ve ilk ekranda Space ayrı ayrı oyunu başlatıyor.
- [ ] Butonla başlattıktan sonra Space odağı doğru alıyor ve kuşu zıplatıyor.
- [ ] Space'i basılı tutmak tekrar tekrar zıplatmıyor; bırakıp yeniden basınca zıplıyor.
- [ ] Game Over sırasında Space'i basılı tutup fareyle Yeniden Başlat'a tıklayınca, Space bırakılmadan kuş zıplamıyor; bırakıp tekrar basınca zıplıyor.
- [ ] Yeniden Başlat'a fareyle tıkladıktan sonra normal Space basışları çalışıyor.
- [ ] Zıplamayan kuş yer çekimiyle düşüyor; üst sınırdan çıkmıyor.
- [ ] Engeller sola hareket ediyor; birkaç tam döngü sonunda sağdan eşit aralıkla geri geliyor.
- [ ] Yeniden gelen engellerin boşlukları dikey olarak değişiyor ve zemini aşmıyor.
- [ ] Her tamamen geçilen engel çifti bir puan veriyor; aynı çift ikinci kez puan vermiyor.
- [ ] Üst engele, alt engele ve zemine çarpma ayrı ayrı oyunu bitiriyor.
- [ ] Game Over sırasında kuş, engeller ve skor birkaç saniye boyunca sabit kalıyor.
- [ ] Yeniden Başlat butonu yalnızca Game Over sonrasında görünüyor.
- [ ] Yeniden başlatınca skor 0, kuş (160, 250), engeller ilk konumlarında ve önceki düşme hızı temizlenmiş.
- [ ] Birkaç yeniden başlatma sonunda oyun hızı değişmiyor ve puanlar doğru sayılıyor.
- [ ] Başka pencereye geçip dönünce Space tekrar çalışıyor; oyun arka planda devam ediyor.
- [ ] Windows %100 ve %150 ekran ölçeklendirmesinde başlık, skor ve butonlar okunuyor.
- [ ] Başlangıç panelinde SkyHop başlığı, kısa açıklama, Oyuna Başla ve Space ipucu ayrı alanlarda görünüyor.
- [ ] Game Over panelinde başlık, son skor ve Yeniden Başlat butonu üst üste gelmiyor.
- [ ] Oynarken sol üstteki skor okunuyor. Kuş ve borular arka plandan rahatça seçiliyor.
- [ ] Boru kapakları geçiş boşluğuna taşmıyor. Boru ve çimen kenarları çarpışma konumlarıyla uyumlu görünüyor.
- [ ] Hareket ve ekran geçişleri sırasında titreme veya eski çizim izi görünmüyor.
- [ ] Oyun sırasında ve Game Over ekranında pencereyi kapatmak hata oluşturmuyor.
- [ ] Gerçek başlangıç görüntüsü `screenshots/01-oyun-baslangic.png` olarak eklendi.
- [ ] Gerçek oynanış görüntüsü `screenshots/02-oyun-devam.png` olarak eklendi.
- [ ] İki gerçek görsel README'ye gömüldü.
