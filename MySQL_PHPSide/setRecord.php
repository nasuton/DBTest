<?php

require_once('mysql_connect.php');

$pdo = connectDB();

function CheckPostNumeric($key)
{
  if(is_numeric($_POST[$key])){
    return (int)$_POST[$key];
  }
  return 0;
}

function CheckPostString($key){
  if(is_string($_POST[$key])){
    return $_POST[$key];
  }
  return "";
}

function CheckPostJson($key){
  if(is_string($key)){
    return json_decode($_POST[$key]);
  }
  return null;
}

function CheckJsonNunmeric($val){
  if(is_numeric($val)){
    return (int)$val;
  }
  return 0;
}

function CheckJsonString($str){
  if(is_string($str)){
    return $str;
  }
  return "";
}

/**
 * 保存するもの
 */
class User {
  public function ToJson() {
    $this->id = CheckJsonNunmeric($this->id);
    $this->name = CheckJsonString($this->name);
    $this->point = CheckJsonNunmeric($this->point);
    return json_encode($this);
  }
}

$postUser = CheckPostJson('user');

header('Content-type:application/json; charset=UTF-8');

//DBからデータを取得する
try {
    $stmt = $pdo->prepare("update shootingdb set name = :name, point = :point where id = :id");
    $stmt->bindValue(':id', $postUser->id, PDO::PARAM_INT);
    $stmt->bindValue(':name', $postUser->name, PDO::PARAM_STR);
    $stmt->bindValue(':point', $postUser->point, PDO::PARAM_INT);
    $stmt->execute();
} catch (PDOException $ex) {
    echo $ex->getMessage();
    exit;
}

$pdo = null;