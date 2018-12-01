<!DOCTYPE html>
<html lang="ru">
<head>
	<!-- Required meta tags -->
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

		<!-- Bootstrap CSS -->
		<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
		<link rel="stylesheet" type="text/css" href="css/main.css">
	<script src="https://api-maps.yandex.ru/2.1/?apikey=0b4b034b-b575-4084-8daf-e77be131e9d9&lang=ru_RU" type="text/javascript"></script>
		<link rel="stylesheet" href="css/style.css">
		<link rel="stylesheet" href="css/left-nav-style.css">
		<link rel="stylesheet" type="text/css" href="css/fab.css">
	<title>Дед доест!</title>
</head>
<body>
		<!-- Main -->
		<main>
			<form action="script-add-food.php" method="post" accept-charset="utf-8">
			<div class="modal-body">
					<div class="input-group input-group-sm mb-3">
						<div class="input-group-prepend">
							<span class="input-group-text" id="inputGroup-sizing-sm">Изображение</span>
						</div>
						<input id="i0" name="i0" type="file" class="file" data-browse-on-zone-click="true">
					</div>
					<div class="input-group input-group-sm mb-3">
						<div class="input-group-prepend">
							<span class="input-group-text" id="inputGroup-sizing-sm">Название</span>
						</div>
						<input type="text" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm" name="i1">
					</div>
					<div class="input-group input-group-sm mb-3">
						<div class="input-group-prepend">
							<span class="input-group-text" id="inputGroup-sizing-sm">Описание</span>
						</div>
						<input type="text" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm" name="i2">
					</div>
					<div class="input-group input-group-sm mb-3">
						<div class="input-group-prepend">
							<span class="input-group-text" id="inputGroup-sizing-sm">Адрес</span>
						</div>
						<input type="text" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm" name="i3" placeholder="Пустое если текущие координаты">
					</div>
			</div>
			<div class="modal-footer">
			<a class="btn btn-secondary" data-dismiss="modal" href="map.html">Закрыть</a>
			<input type="submit" class="btn btn-primary" value="Дать деду!">
			</form>
		</main>
		
		<!-- Modal -->
		
		<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
		<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
		<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
	<script src="js/ymap.js" type="text/javascript"></script>
</body>
</html>