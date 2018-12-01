<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    <link rel="stylesheet" href="css/main.css">
    <style>
    body{
    margin: 0;
}

a{
    color: #000 !important;
}

.svg{
    width: 100%;
    margin: 0 auto;
}

svg{
    margin-left: 40%;
}

.area{
    width: 100%;
}

.bg-red{
    background-color: red;
}

.bg-green{
    background-color: green;
}

.square{
    width: 300px;
    height: 300px;
    margin-top:40px;
    margin-bottom: 40px;
    margin-left: 40%;
}

.square a{
    display: block;
    width: 100%;
    text-align: center;
    padding-top: 40%;
}

.post{
    width: 100%;
    height: 300px;
    border: 2px solid;
    border-color: #000;
    margin-top: 30px;
    margin-bottom: 30px;
}

.photo{
    width: 100%;
    height: 295px;
    background-size: cover;
}

.desk{
    margin-top: 10%;
    list-style: none;
}


.desk li{
    margin-bottom: 30px;
}
    </style>
    <title>Document</title>
</head>
<body>
    <div class="container">
        <?php
            $DB_LINK = mysqli_connect('tarangok.ru', 'tarangok', '123555', 'tarangok_db');
            $result = mysqli_query($DB_LINK, "SELECT * FROM `db`");
            while($row = mysqli_fetch_assoc($result))
            {
                echo "<div class='post'>
            <div class='row'>
                <div class='col-lg-4'>
                    <img class='photo' src=".$row['photo']." alt='Photo'>
                </div>
                <div class='col-lg-8'>
                    <ul class='desk'>
                        <li>Название: ".$row['name']."</li>
                        <li>Описание: ".$row['data']."</li>
                        <li>Координаты: ".$row['addr']."</li>
                    </ul>
                </div>
            </div>
        </div>";
            }
        ?>
        <a class="btn btn-danger" href="index.php" style="margin-left: 30vw;">Назад</a>
    </div>

</body>
</html>

        