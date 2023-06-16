# Peki Worms Clone

## Bu proje Unity'nin hiçbir fizik tabanlı sistemi kullanılmadan geliştirilmiştir.

![Screenshot](ReadMeImages/Worm.png)

## Kurtların her objesine ayrı ayrı bakmak yerine kurdun merkezinden hayali bir circle collider oluşturdum.

![Square](ReadMeImages/square.png)

### Oyun alanını hayali karelere bölebildiğim bir method hazırladım.

Yaptığım testler sonucunda en optimum değerin 16 olduğunu buldum. Oyun alanını 16 kareye böldüm. Kurtlar sadece kendilerinin bulunduğu karedeki diğer kurtlarla çarpışma testi yapıyor. Böylece bir kurt oyundaki kurtların düşük bir yüzdesiyle çarpışma testi yapıyor. Bu çarpışma testlerinde ilk olarak ilk resimde gösterdiğim hayali çemberlerle çarpışıp çarpışmadığına bakıyor. Eğer bir çarpışma mevcut ise geriye kalan durumları test ediyor. Bir çarpışma bulunmaz ise testi yapan kurt diğer kurda haber veriyor. Böylece diğer kurt

![Square](ReadMeImages/yenipng.png)

## Resimde görüldüğü gibi eğer uzaklık iki çemberin yarı çaplarının toplamından büyük ise kontrol edilmez.

![Square](ReadMeImages/new2.png)

## Eğer çemberler birbirlerine temas ederlerse bu sefer kurtların vücut ve baş bölgeleri birbirlerini kontrol etmeye başlarlar. Kurtlardan birisinin baş bölgesi diğer kurdun vücut bölgesine çarpsa baş bölgesiyle çarpan kurt başlangıç boyutuna döndürülüp oyuncunun ekranı dışındaki bir yere ışınlanır. 



Apk : https://github.com/FurkanSertfs/WormsClone/blob/main/Apk/WormsClone.apk


