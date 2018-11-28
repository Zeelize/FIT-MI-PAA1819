set terminal png
set xlabel "maximální váha předmětů"
set ylabel "počet výpočetních stavů"
plot "data.txt" using 1:2 with lines title "Průměrných výpoč. stavu pro 25 předmětů"