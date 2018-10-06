set terminal png
set xlabel "počet předmětů (n)"
set ylabel "maximální relativní chyba (%)"
set style fill solid 1.00 border
set boxwidth 1.2
plot "maxMistakes.dat" using 1:2 with boxes title "Maximální relativní chyba pro <n> předmětů"