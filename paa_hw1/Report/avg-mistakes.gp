set terminal png
set xlabel "počet předmětů (n)"
set ylabel "průměrná relativní chyba (%)"
set style fill solid 1.00 border
set boxwidth 1.2
plot "avgMistakes.dat" using 1:2 with boxes title "Průměrná relativní chyba pro <n> předmětů"