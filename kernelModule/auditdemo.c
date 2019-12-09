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

#define DES_KEY_SIZE 8

FILE *key_file;

void encrypted_write(char* filepath,char* buffer,char* keypath)
{
	key_file = fopen(keypath, "rb");
	short int bytes_read;
	unsigned char* des_key = (unsigned char*) malloc(8*sizeof(char));
	bytes_read = fread(des_key, sizeof(unsigned char), DES_KEY_SIZE, key_file);
	if (bytes_read != DES_KEY_SIZE) {
		printf("Key read from key file does nto have valid key size.");
		fclose(key_file);
		return ;
	}
	fclose(key_file);
	syscall(250,filepath,buffer,strlen(buffer),des_key);//调用新系统调用
	free(des_key);
}
int decrypted_read(char* filepath,char* buffer,char* keypath)
{
	FILE *encrypted_file;
	encrypted_file=fopen(filepath,"rb");
	unsigned long file_size;
	fseek(encrypted_file, 0L, SEEK_END);
	file_size = ftell(encrypted_file);
	fclose(encrypted_file);
	key_file = fopen(keypath, "rb");
	short int bytes_read;
	unsigned char* des_key = (unsigned char*) malloc(8*sizeof(char));
	bytes_read = fread(des_key, sizeof(unsigned char), DES_KEY_SIZE, key_file);
	if (bytes_read != DES_KEY_SIZE) {
		printf("Key read from key file does nto have valid key size.");
		fclose(key_file);
		return 0;
	}
	fclose(key_file);
	syscall(249,filepath,buffer,file_size,des_key);
	free(des_key);
	return 1;
}

int main(int argc, char *argv[]){
	char* filepath="test.txt";
	char* buffer="Just for test!!!";
	char* keypath="key";
	encrypted_write(filepath,buffer,keypath);
	int flag;
	char read_buf[512];
	flag=decrypted_read(filepath,read_buf,keypath);
	printf("decrypted content:%s, length=%d",read_buf,strlen(read_buf));
	return 0;
}

