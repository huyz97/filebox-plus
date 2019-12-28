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

void decrypted_copy(char* src_enc_file_path,char* dst_dec_file_path,char* des_key_path)//将加密的srcfile拷贝到dstfile，并解密
{
	//load des_key
	FILE *key_file;
	key_file = fopen(des_key_path, "rb");
	short int bytes_read;
	unsigned char* des_key = (unsigned char*) malloc(8*sizeof(char));
	bytes_read = fread(des_key, sizeof(unsigned char), DES_KEY_SIZE, key_file);
	
	FILE *src_file;//为了获取源文件的size
	src_file=fopen(src_enc_file_path,"rb");
	unsigned long file_size;
	fseek(src_file, 0L, SEEK_END);
	file_size = ftell(src_file);
	syscall(249,src_enc_file_path,dst_dec_file_path,file_size,des_key);//调用新系统调用
	free(des_key);
	fclose(key_file);
	fclose(src_file);
	
}

int main(int argc, char *argv[]){
	//生成decrypted_copy.out
	char* src_enc_file_path=argv[1];
	char* dst_dec_file_path=argv[2];
	char* des_key_path=argv[3];
	decrypted_copy(src_enc_file_path,dst_dec_file_path,des_key_path);
	return 0;
}

