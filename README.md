# rfid_redis_sample
## 適当なディレクトリでRedisサーバ起動
```
$ redis-server
```
確認方法

## Client起動
cf.) http://mayo.hatenablog.com/entry/2013/10/15/074237
```
$ redis-cli
#keyの一覧が出るので見たい奴を選んで次のKEYにいれる
$ keys *  
$ get KEY
#リストの中身を見る(1~100まで)
lrange KEY 0 100

```
