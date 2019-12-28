#include <sys/stat.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <linux/netlink.h>
#include <linux/socket.h>
#include <fcntl.h>
#include <asm/types.h>
#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <signal.h>
#include <pwd.h>

void generate_key(char* key_path)
{
	short int bytes_written;
	FILE *key_file;
	key_file=fopen(key_path,"wb");
	unsigned char* des_key = (unsigned char*) malloc(8*sizeof(char));
	int i;
	unsigned int iseed = (unsigned int)time(NULL);
	srand (iseed);
	for (i=0; i<8; i++) {
		des_key[i] = rand()%255;
	}
	bytes_written = fwrite(des_key, 1, DES_KEY_SIZE, key_file);
	free(des_key);
	fclose(key_file);
	return;
}