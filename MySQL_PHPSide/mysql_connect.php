<?php
//PDO MySQLに接続
function connectDB(){
  //ユーザー名やDBアドレスを定義
  $db_user = "*****";         //DBログイン時のユーザー名
  $db_pass = "********"; 　   //DBログイン時のパスワード
  $db_host = "localhost";     //ホスト名
  $db_name = "******";        //データベース名("use ～"で使う名前)
  $db_type = "mysql";         //データベースの種類(今回はMySQLを使用しているため)

  $dsn = "$db_type:host=$db_host;dbname=$db_name;cherset=utf8";

  try{
    $pdo = new PDO($dsn, $db_user, $db_pass);
    //エラーモードの設定
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  }catch(PDOException $ex){
    die('エラー:'.$ex->getMessage());
  }

  return $pdo;
}
