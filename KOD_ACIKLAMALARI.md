# SkyHop — Kod Açıklamaları

## 1. Timer ne işe yarıyor?

`Form1` içindeki `gameTimer`, yaklaşık 20 milisaniyede bir `GameTimer_Tick` metodunu çağırır. Bu metot kuşu ve engelleri hareket ettirir, çarpışmayı ve skoru kontrol eder. `Invalidate()` ekranın yeniden çizilmesini ister. Timer kesin zamanlı bir fizik motoru değildir.

## 2. KeyDown olayı nedir?

Bir tuşa basıldığında oluşan olaydır. `Form1_KeyDown`, basılan tuşun `Keys.Space` olup olmadığını kontrol eder. `KeyPreview = true`, formun klavye olaylarını kontrollerden önce almasını sağlar. `spacePressed` ve `KeyUp`, basılı tutulan Space'in sürekli zıplatmasını engeller.

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

Yeniden Başlat butonu `StartGame()` metodunu çağırır. Önce Timer durdurulur. Skor sıfırlanır, `bird.Reset()` kuşu başlangıca getirip hızını sıfırlar. Döngüyle bütün engeller ilk konum ve boşluklarına döner, puan bayrakları temizlenir. Oyun durumu ve Space bilgisi sıfırlanır, butonlar gizlenir, odak forma verilir ve Timer yeniden başlar.
