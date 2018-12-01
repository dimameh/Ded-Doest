<?php
	$name = $_POST['i1'];
	$data = $_POST['i2'];
	$addr = $_POST['i3'];
	$DB_LINK = mysqli_connect('tarangok.ru', 'tarangok', '123555', 'tarangok_db');
	if ($DB_LINK)
	{
		echo "DB-1";
	}
	
	$result = mysqli_query($DB_LINK, "INSERT INTO `db`(`name`, `data`, `addr`, `photo`) VALUES ('$name','$data','$addr', '$photo')");
	if ($result)
	{
		header('Location: http://tarangok.ru/list.php');
	}
	else
	{
		echo "ДА БЛИИН!";
	}
?>