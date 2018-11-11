<?php
//PDO MySQLに接続
function connectDB(){
  //ユーザー名やDBアドレスを定義
  $db_user = "sample";        //ユーザー名
  $db_pass = "vantan1506";    //パスワード
  $db_host = "localhost";     //ホスト名
  $db_name = "sampledb";      //データベース名
  $db_type = "mysql";         //データベースの種類

  $dsn = "$db_type:host=$db_host;db_name=$db_name;cherset=utf8";

  try{
    $pdo = new PDO($dsn, $db_user, $db_pass);
    //エラーモードの設定
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  }catch(PDOException $ex){
    die('エラー:'.$ex->getMessage());
  }

  return $pdo;
}
