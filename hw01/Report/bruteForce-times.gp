set terminal png
set xlabel "počet předmětů (n)"
set ylabel "čas (ms)"
set style fill solid 1.00 border
set xrange [0:27]
set yrange [0.01:90000]
set logscale y
set boxwidth 1.2
set output "bfT.png"
plot "bfTimes.dat" using 1:2 with boxes title "Průměrný čas pro <n> předmětů"
