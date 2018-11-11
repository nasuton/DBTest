<?php
//下記URLを参照すること
//https://qiita.com/kemmimilk/items/9da751e88e0b6aefaa62

require_once('mysql_connect.php');

$pdo = connectDB();

//POST受け取り
$id = $_POST["id"];    //要求されてくるid

try{
  //今回はここでSELECT文を送信している
  $stmt = $pdo->query("SELECT * FROM 'unity' WHERE 'id' = '". $id. "'");
  foreach ($stmt as $row) {
    //今回はただカラムを指定して、出力された文字列を結合して出力
    $res = $row['id'];
    $res = $res.$row['name'];
    $res = $res.$row['point'];
    $res = $res.$row['data'];
  }
}catch (PDOException $ex){
  var_dump($ex->getMessage());
}

//DB切断
$pdo = null;

//unityに結果を返す
echo $res;
