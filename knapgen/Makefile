CXX     = gcc
LD      = gcc
CFLAGS  = -Wall -pedantic
CLIBS	= -lm
DELFILE = rm -f

all: generator

generator: knapcore.o knapgen.o
	$(LD) -o generator knapcore.o knapgen.o $(CLIBS)

knapcore.o: knapcore.c knapcore.h
	$(CXX) $(CFLAGS) -c -o knapcore.o knapcore.c $(CLIBS)

knapgen.o: knapgen.c knapgen.o
	$(CXX) $(CFLAGS) -c -o knapgen.o knapgen.c $(CLIBS)

clean:
	$(DELFILE) knapcore.o knapgen.o generator *.core

