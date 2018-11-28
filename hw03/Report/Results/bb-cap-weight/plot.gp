set terminal png
set xlabel "poměr kapacity k sumární váze"
set ylabel "počet výpočetních stavů"
plot "data.txt" using 1:2 with lines title "Průměrných výpoč. stavu pro 25 předmětů"