# WormsClone

![Furkan1](https://github.com/FurkanSertfs/WormsClone/assets/69736742/67f96f73-7faa-401f-ac18-f94abbb3d3ca)

Kurtların her objesine ayrı ayrı bakmak yerine kurdun merkezinden hayali bir circle collider oluşturdum.

![square](https://github.com/FurkanSertfs/WormsClone/assets/69736742/bd0053be-0877-4cb7-a712-2dfc883b223e)

Oyun alanını hayali karelere bölebildiğim bir method hazırladım. Yaptığım testler sonucunda en optimum değerin 16 olduğunu buldum. Oyun alanını 16 kareye böldüm. Kurtlar sadece kendilerinin bulunduğu karedeki diğer kurtlarla çarpışma testi yapıyor. Böylece bir kurt oyundaki kurtların düşük bir yüzdesiyle çarpışma testi yapıyor. Bu çarpışma testlerinde ilk olarak ilk resimde gösterdiğim hayali çemberlerle çarpışıp çarpışmadığına bakıyor. Eğer bir çarpışma mevcut ise geriye kalan durumları test ediyor. Bir çarpışma bulunmaz ise testi yapan kurt diğer kurda haber veriyor. Böylece diğer kurt gereksiz yere test yapmıyor. Yiyecekler ve güçlendirmeler içinde aynı sistemi kullanıyorum.


