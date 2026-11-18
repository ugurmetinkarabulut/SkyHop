# SkyHop — Kod Açıklamaları

## 1. Timer ne işe yarıyor?

`Form1` içindeki `gameTimer`, yaklaşık 20 milisaniyede bir `GameTimer_Tick` metodunu çağırır. Bu metot kuşu ve engelleri hareket ettirir, çarpışmayı ve skoru kontrol eder. `Invalidate()` ekranın yeniden çizilmesini ister. Timer kesin zamanlı bir fizik motoru değildir.

## 2. KeyDown olayı nedir?

Bir tuşa basıldığında oluşan olaydır. `Form1_KeyDown`, basılan tuşun `Keys.Space` olup olmadığını kontrol eder. `KeyPreview = true`, formun klavye olaylarını kontrollerden önce almasını sağlar. `spacePressed` ve `KeyUp`, basılı tutulan Space'in sürekli zıplatmasını engeller.

`KeyDown` içinde `e.Handled = true` kullanılır. Space karakteri ayrıca `Form1_KeyPress` içinde işlenir. `SuppressKeyPress` kullanılmaz, çünkü Microsoft belgeleri bu özelliğin `KeyUp` olayını da bastırdığını belirtir. `KeyUp` geldiğinde `spacePressed = false` olur ve sonraki basış yeniden zıplatır. [Microsoft klavye olayları belgesi](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/input-keyboard/events)

## 3. Kuşun zıplamasını nasıl sağladık?

Space'e basılınca `bird.Jump()` çağrılır. `Bird.Jump()`, `VerticalSpeed` değerini `-7.5f` yapar. Ekranda Y değeri aşağı doğru arttığı için negatif hız kuşu yukarı taşır.

## 4. Yer çekimini nasıl oluşturduk?

`Bird.Update()` her adımda dikey hıza `0.45f` ekler, ardından bu hızı Y konumuna ekler. Zıplamanın negatif hızı zamanla sıfıra yaklaşır, sonra pozitif olur ve kuş düşer. Üst sınır aşılırsa Y ve hız sıfırlanır.

## 5. Engeller nasıl hareket ediyor?

`List<Pipe>` içinde üç engel çifti tutulur. Timer içindeki `for` döngüsü her çiftin `Move()` metodunu çağırır. Bu metot X konumundan 4 çıkarır. Tamamen soldan çıkan engel, en sağdaki engelden 290 piksel uzağa taşınır. `Random.Next` yeni boşluğun dikey konumunu seçer.

## 6. Çarpışmayı nasıl kontrol ediyoruz?

`Bird.Bounds` kuşun dikdörtgenini, `Pipe.TopBounds` ve `BottomBounds` engellerin dikdörtgenlerini verir. `CheckCollision()` içindeki `IntersectsWith`, kuşun engellerle kesişip kesişmediğine bakar. Kuşun alt kenarı `GroundY` değerine ulaşırsa zemin çarpışması olur. `EndGame()` Timer'ı durdurur ve Game Over ekranını gösterir.

## 7. Skor nasıl artıyor?

Engelin sağ kenarı kuşun sol kenarını geçtiğinde ve `Scored` değeri `false` olduğunda `score++` çalışır. Ardından `Scored = true` yapılır; aynı engelden tekrar puan alınmaz. Engel sağa taşınırken `Pipe.Reset()` bu bayrağı temizler. Önce çarpışma kontrol edildiği için ölüm anında yeni puan verilmez.

## 8. Bird ve Pipe sınıflarını neden ayırdık?

Kuşun hareket bilgileri `Bird` içinde, engel çiftinin bilgileri `Pipe` içinde toplanır. `Form1` bu nesneleri kullanarak oyunu yönetir. Böylece her sınıfın görevi anlaşılır olur; ek bir oyun motoru sınıfına ihtiyaç kalmaz.

## 9. Constructor nedir?

Nesne `new` ile oluşturulurken çalışan, sınıfla aynı isimde ve dönüş tipi olmayan özel metottur. Örneğin `new Bird(160, 250)` kuşun başlangıç konumunu ayarlar. `Pipe` constructor'ı konumu, boşluğu ve zemin yüksekliğini alır. `Form1()` ise pencereyi, butonları, nesneleri ve Timer'ı oluşturur.

## 10. Yeniden başlatma nasıl çalışıyor?

Yeniden Başlat butonu `StartGame()` metodunu çağırır. Önce Timer durdurulur. Skor sıfırlanır, `bird.Reset()` kuşu başlangıca getirip hızını sıfırlar. Döngüyle bütün engeller ilk konum ve boşluklarına döner, puan bayrakları temizlenir. Oyun durumu sıfırlanır, butonlar gizlenir, odak forma verilir ve Timer yeniden başlar. Space basılıysa bu bilgi korunur; tuş bırakılmadan gelen tekrar olayları yeni zıplama sayılmaz.

## 11. Oyun ekrana nasıl çiziliyor?

- `OnPaint`, pencerenin içi yeniden çizilirken çalışır. Sırayla gökyüzünü, engelleri, zemini ve kuşu çizer. Oyun durumuna göre skor kutusunu ya da menü panelini ekler.
- `e.Graphics`, formun çizim yüzeyine erişim verir. `FillEllipse` kuşun gövdesini, `FillRectangle` boruları ve `DrawString` metinleri çizer. Oluşturduğumuz fırça, kalem ve yazı tiplerini `using` ile iş bitince serbest bırakırız. İki butonun paylaştığı yazı tipi pencere kapanırken serbest bırakılır.
- `Invalidate()`, Windows Forms'tan pencereyi yeniden çizmesini ister. Böylece Timer konumları güncelledikten sonra kuş ve borular yeni yerlerinde görünür.
- `DoubleBuffered`, çizimi önce bellekte hazırlayıp sonra ekrana aktarır. Oyun hareket ederken oluşabilecek titremeyi azaltır.

## 12. public ve private farkı nedir?

`public` üyeye başka sınıflar erişebilir. `Bird.Jump()` bu yüzden `Form1` tarafından çağrılabilir. `private` üyeye yalnızca kendi sınıfı erişir. `Form1` içindeki `score` alanını oyun formu yönetir.

## 13. const ve readonly farkı nedir?

`const` değeri derlenirken belirlenir ve değişmez. `Bird.Width = 38` buna örnektir. `readonly` alan tanımlanırken veya constructor içinde atanır. `bird`, `Form1()` içinde oluşturulur ve daha sonra başka bir kuş nesnesiyle değiştirilemez. Kuş nesnesinin konumu yine değişebilir.

## 14. get ve private set neden kullanılır?

`Bird.Y` özelliğindeki `get`, formun kuşun konumunu okumasını sağlar. `private set`, konumu değiştirme yetkisini `Bird` sınıfında tutar. `Update()` ve `Reset()` bu değeri değiştirir. `Bounds` ise yalnızca `get` içerir ve mevcut konumdan bir `Rectangle` hesaplar.

## 15. `List<Pipe>` neden kullanılır?

`pipes`, aynı türden birden fazla engel çiftini tutan listedir. Constructor içindeki `Add` çağrıları üç engel çifti ekler. `for` döngüsü ve `pipes.Count` sayesinde bu çiftlerin hepsi hareket ettirilir ve çarpışma için kontrol edilir.

## 16. Event Handler nedir?

Bir olay gerçekleştiğinde çağrılan metottur. `gameTimer.Tick += GameTimer_Tick`, Timer olayını oyun döngüsü metoduna bağlar. `restartButton.Click += RestartButton_Click` ise butona tıklanınca yeniden başlatmayı sağlar. `sender` olayı oluşturan nesneyi, `e` olayın bilgilerini taşır.

## 17. Rectangle ve IntersectsWith ne işe yarar?

`Rectangle`, X, Y, genişlik ve yükseklik ile bir dikdörtgen tanımlar. `Bird.Bounds` kuşu, `Pipe.TopBounds` ve `BottomBounds` engelleri temsil eder. `birdBounds.IntersectsWith(pipes[i].TopBounds)` kuşun üst engelle kesişip kesişmediğini döndürür.

## 18. override neden kullanılır?

Üst sınıfın sunduğu bir metodu kendi sınıfımız için uyarlamamızı sağlar. `Form1 : Form`, Windows Forms'un `Form` sınıfından türemiştir. `OnPaint` metodu `override` edilerek oyunun çizimleri eklenir. `base.OnPaint(e)` üst sınıfın çizim olayını da çalıştırır.

## 19. Dispose ne işe yarar?

Bir nesnenin kullandığı kaynakları serbest bırakır. `Form1.Dispose(bool disposing)` içinde `gameTimer.Dispose()` Timer'ı, `menuButtonFont.Dispose()` ortak buton yazı tipini temizler. `base.Dispose(disposing)` formun kendi kontrollerini temizlemesini sağlar.

## 20. using ile kaynak yönetimi nasıl yapılır?

`using SolidBrush sun = new SolidBrush(...)` gibi bir tanımlamada fırça metottan çıkarken otomatik olarak serbest bırakılır. Çizimlerde oluşturulan `Font`, `Pen` ve `StringFormat` nesneleri de bu şekilde temizlenir. `Brushes.White` gibi hazır paylaşılan fırçaları ve Windows'un verdiği `e.Graphics` nesnesini biz oluşturmadığımız için serbest bırakmayız. Dosyanın başındaki `using System.Drawing;` ise sınıf adlarını kısa yazmamıza yarar.
