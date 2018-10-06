set terminal png
set xlabel "počet předmětů (n)"
set ylabel "čas (ms)"
set style fill solid 1.00 border
set boxwidth 1.2
plot "pwTimes.dat" using 1:2 with boxes title "Průměrný čas pro <n> předmětů"