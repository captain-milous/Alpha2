# Alpha2 (Kompresor a Dekompresor Textových Souborů)
## Popis
Tato konzolová aplikace napsaná v jazyce C# slouží k efektivní kompresi a dekompresi textových souborů. Program využívá slovník kompresních zkrat, který umožňuje nahrazení opakujících se slov zkrácenými klíči, což vede ke snížení velikosti textových dat.

## Spuštění
### Předpoklady
Pro správné spuštění programu je nutné být na počítačovém zařízení Windows 10 a více

### Postup
Postupujte následovně:

1. Stáhněte si zazipovaný soubor do svého počítače.
2. Soubor Extrahujte.
3. Přesuňte se do adresáře: *Alpha2\Komprese\bin\Debug\net7.0\ *
4. Spusťte aplikaci: "Komprese.exe"

### Konfigurace
Program vyžaduje konfigurační soubor pro nastavení cest k vstupním a výstupním souborům, cesty ke slovníku kompresních zkrat a dalších parametrů. Konfigurační soubor je ve formátu XML. 

**Cesta ke konfiguračnímu souboru:**
*Komprese\bin\Debug\net7.0\config\config.xml*

## Vstupní Data
Program pracuje s textovými soubory, které jsou vhodné k kompresi a dekompresi. Před spuštěním programu zajistěte, že vstupní soubor obsahuje textová data ve formátu, který chcete komprimovat a dekomprimovat.

## Ovládání Programu
Po spuštění programu se zobrazí uživatelské rozhraní v příkazovém řádku. Uživatel může používat následující příkazy:

- help: Zobrazí seznam dostupných příkazů.
- input: Změní cestu k souboru, ze kterého chcete provést kompresi/dekompresi.
- output: Změní cestu k souboru, do kterého chcete provést kompresi/dekompresi.
- compress: Provede kompresi textového souboru.
- decompress: Provede dekompresi textového souboru.
- exit: Ukončí program.

## Logika Kompresní a Dekompresní Operace
Program využívá slovník kompresních zkratek k identifikaci opakujících se slov v textu. Při kompresi se každé slovo nahrazuje odpovídajícím zkráceným klíčem. Při dekompresi dochází k opačnému procesu, kde každý zkrácený klíč je nahrazen původním slovem.

Logika komprese a dekomprese je založena na minimalizaci opakování slov v textu a jejich nahrazení zkrácenými klíči. Slovník kompresních zkrat se ukládá do XML souboru pro udržení stavu mezi různými běhy programu.

## Autor
Miloš Tesař C4b

[Inflectra Corporation](https://github.com/captain-milous/Alpha2)