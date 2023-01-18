.data
;tablica wag zgodna z rozk³adem Gaussa, przystosowana do operacji wektorowych
gaussMatrix:
word 1
word 1
word 1
word 2
word 2
word 2
word 1
word 1
word 1
word 2
word 2
word 2
word 4
word 4
word 4
word 2
word 2
word 2
word 1
word 1
word 1
word 2
word 2
word 2
word 1
word 1
word 1


.code

;Funkcja rozmywaj¹ca obraz metod¹ Gaussa
;
;Funkcja posiada cztery parametry wejœciowe: pocz¹tek tablicy przechowuj¹cej dane bitmapy, rozmiar bitmapy, oraz numery wierszy,
;które okreœlaj¹ zakres wykonania rozmycia (umo¿liwia wykonywanie wielow¹tkowe)
;Funkcja modyfikuje zawartoœæ tablicy z danymi bitmapy(wartoœci pixeli)
;bmp -> rcx, bmpSize -> rdx, imageWidth -> r8, startHeight -> r9, endHeight -> stack
;
;Rejestry, które ulegaj¹ zmianie: rax, rbx, rcx, rdx, rsi, rdi r8, r9, r10, r11, r12, r13, r14, r15, xmm0-14, rip, efl
;
;Flagi, które ulegaj¹ zmianie: ZR, CY, PE, PL, AC
;
;Dziêki wykorzystaniu rejestrów xmm, dzia³ania s¹ wykonywane jednoczeœnie na sk³adowych RGB piksela, a nie jak w przypadku C++,
;gdzie na ka¿dej sk³adowej s¹ wykonywane oddzielnie
gaussBlurAsm proc

;rejestry od xmm0 do xmm5 zawieraja w sobie odpowiednie wagi pobrane z macierzy potrzebne do obliczenia sumy wazonej
movdqu xmm0, oword ptr[gaussMatrix]
movdqu xmm1, oword ptr[gaussMatrix + 6]
movdqu xmm2, oword ptr[gaussMatrix + 12]
movdqu xmm3, oword ptr[gaussMatrix + 18]
movdqu xmm4, oword ptr[gaussMatrix + 24]
movdqu xmm5, oword ptr[gaussMatrix + 30]

;wyczyszczenie rejestru r10
xor r10, r10
;pobranie ze stosu i zapisanie do rejestru r10 numeru wiersza, na którym algorytm ma poprzestaæ wykonywania siê(endHeight)
mov r10d, dword ptr[rbp+48]

;zapisanie do rejestru r15 adresu poczatku bitmapy
mov r15, rcx

;wczytanie do akumulatora szerokosci bitmapy
mov rax, r8
;pomnozenie szerokosci bitmapy razy 3 (uwzglêdnienie zapisu piksela RGB)
imul rax, 3

;zapisanie praktycznej szerokosci do rejestru rdi(zapisanie zawartosci rejestru rax do rejestru rdi)
mov rdi, rax

;pomnozenie praktycznej szerokosci przez numer wiersza, od którego algorytm ma wykonaæ algorytm
imul rax, r9  ;r9 przechowyuje numer wiersza, od którego algorytm ma wykonaæ algorytm

;przeniesie zawartosci rejestru rax do rejestru rcx
;rcx bedzie sluzyl jako licznik petli (odpowiednik "int i" w functions.cpp)
mov rcx, rax

;wypelnienie akumulatora wartoscia praktycznej szerokosci(rejestru rdi)
mov rax, rdi
;pomnozenie akumulatora przez numer wiersza, na którym algorytm ma poprzestaæ wykonywania siê
imul rax, r10
;przeniesienie wartosci z akumulatora do rejestru r12
;rejestr r12 bedzie sluzyl do sprawdzania, czy petla ma sie dalej wykonywac(czy jest to koniec bitmapy/odcinka bitmapy)
mov r12, rax

mainLoop:

;inkrementacja licznika
add rcx, 3

;PIERWSZY WARUNEK
;wypelnienie akumulatora wartoscia licznika
mov rax, rcx
;odjecie praktycznej szerokosci od wartosci licznika
sub rax, rdi
;odjecie 3 od zawartosci akumulatora
sub rax, 3
;sprawdzenie czy zawartosc akumulatora nie jest mniejsza od zera
;jest to sprawdzenie, czy lewy gorny sasiad aktualnego pixela nie jest poza tablica
cmp rax, 0
;jesli zawartosc jest mniejsza to wroc na poczatek petli
jl mainLoop

;DRUGI WARUNEK
;wypelnienie akumulatora wartoscia licznika
mov rax, rcx
;dodanie praktycznej szerokosci od wartosci licznika
add rax, rdi
;dodanie 3 od zawartosci akumulatora
add rax, 3
;sprawdzenie czy zawartosc akumulatora jest wieksza od rozmiaru bitmapy
;jest to sprawdzenie, czy prawy dolny sasiad aktualnego pixela nie jest poza tablica
cmp rax, rdx ;rdx przechowuje rozmiar bitmapy
;jesli zawartosc jest wieksza lub rowna rozmiarowi bitmapy to idz do punktu zakonczenia wykonywanie procedury
jae Koniec

;GLOWNA CZESC FUNKCJI

;przeniesienie poczatku bitmapy do akumulatora
mov rax, r15
;dodanie licznika do akumulatora
add rax, rcx 
;odjecie praktycznej szerokosci od akumulatora
sub rax, rdi
;odjecie 3 od zawartosci akumulatora
sub rax, 3
;w akumulatorze znajduje sie adres pod ktorym znajduje sie wartosc lewego gornego sasiada aktualnego pixela
;aktualny pixel to pixel o indexie licznika(w c++ to bmp[i], a lewy gorny sasiad to w c++ bmp[i-width-3])

;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm6
pmovzxbw xmm6, [rax]
; dodanie do akumulatora wartosci 3
; skutkuje to przesunieciem sie o jeden pixel w prawo
; tzn. jesli w rzedzie mamy pixele: p1, p2, p3, p4, p5 (kazdy pixel sklada sie z 3 skladowych, czyli pixel p1 to tak naprawde 3 skladowe r1, g1, b1)
; to w xmm6 znajduja sie: r1, g1, b1, r2, g2, b2, r3, g3
; a w xmm7 beda to:       r2, g2, b2, r3, g3, b3, r4, g4
; analogicznie w xmm8:    r3, g3, b3, r4, g4, b4, r5, g5
add rax, 3
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm7
pmovzxbw xmm7, [rax]
; dodanie do akumulatora wartosci 3
add rax, 3
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm8
pmovzxbw xmm8, [rax]

;dodanie do akumulatora praktycznej szerokosci
;z punktu widzenia algorytmu jest to przejscie o rzad nizej w bitmapie
add rax, rdi
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm9
pmovzxbw xmm9, [rax]
;odjecie od akumulatora wartosci 3
sub rax, 3
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm10
pmovzxbw xmm10, [rax]
;odjecie od akumulatora wartosci 3
sub rax, 3
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm11
pmovzxbw xmm11, [rax]


;dodanie do akumulatora praktycznej szerokosci
;z punktu widzenia algorytmu jest to przejscie o rzad nizej w bitmapie
add rax, rdi
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm12
pmovzxbw xmm12, [rax]
;dodanie do akumulatora wartosci 3
add rax, 3
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm13
pmovzxbw xmm13, [rax]
;dodanie do akumulatora wartosci 3
add rax, 3
;przeniesienie 8 kolejnych skladowych pixeli do rejestru xmm14
pmovzxbw xmm14, [rax]

;wszystkie operacje pmullw maja za zadanie przemnozenie wszystkich wartosci pixeli przez odpowiednie wagi
pmullw xmm6, xmm0
pmullw xmm7, xmm1
pmullw xmm8, xmm2

pmullw xmm9, xmm3
pmullw xmm10, xmm4
pmullw xmm11, xmm5

pmullw xmm12, xmm0
pmullw xmm13, xmm1
pmullw xmm14, xmm2


;zsumowanie wszystkich pixeli przemnozonych wczesniej przez odpowiednie wagi
;wynikiem beda sumy wazone znajdujaca sie w xmm6
paddw xmm6, xmm7
paddw xmm6, xmm8
paddw xmm6, xmm9
paddw xmm6, xmm10
paddw xmm6, xmm11
paddw xmm6, xmm12
paddw xmm6, xmm13
paddw xmm6, xmm14

;podzielenie sum wazonych przez 16
psrlw xmm6, 4

;skopiowanie do rejestru r9 pierwszego 2-bajtowego slowa z rejestry xmm6
pextrw r9, xmm6, 0
;rejestr r9 przechowuje teraz wartosc skladowa wynikowego pixela
;skopiowanie do rejestru r13 drugiego 2-bajtowego slowa z rejestry xmm6
pextrw r13, xmm6, 1
;rejestr r13 przechowuje teraz wartosc skladowa wynikowego pixela
;skopiowanie do rejestru r14 trzeciego 2-bajtowego slowa z rejestry xmm6
pextrw r14, xmm6, 2
;rejestr r14 przechowuje teraz wartosc skladowa wynikowego pixela

;skopiuj do rejestru r11, poczatek bitmapy
mov r11, r15
;dodaj wartosc licznika do poczatku bitmapy
add r11, rcx 

;zmodyfikuj wartosc pierwszej skladowej pixela
mov byte ptr[r11], r9b
;inkrementuj rejestr r11
add r11, 1
;zmodyfikuj wartosc drugiej skladowej pixela
mov byte ptr[r11], r13b
;inkrementuj rejestr r11
add r11, 1
;zmodyfikuj wartosc trzeciej skladowej pixela
mov byte ptr[r11], r14b

;sprawdz czy licznik jest wiekszy lub rowny wartosci zakonczenia petli
cmp rcx, r12
;jesli tak to przejdz do punktu zakonczenia procedury
jae Koniec
;jesli nie to przejdz do poczatku petli(wykonuj petle dalej)
jmp mainLoop

;punkt zakoñczenia procedury
Koniec:
;powrót z procedury
ret

;Koniec procedury rozmycia obrazu
gaussBlurAsm endp

end