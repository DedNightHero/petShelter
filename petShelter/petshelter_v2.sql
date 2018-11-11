-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1
-- Время создания: Ноя 11 2018 г., 09:28
-- Версия сервера: 10.1.36-MariaDB
-- Версия PHP: 7.2.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `petshelter`
--

-- --------------------------------------------------------

--
-- Структура таблицы `animals`
--

CREATE TABLE `animals` (
  `Id_Animals` int(10) UNSIGNED NOT NULL,
  `Species` int(10) UNSIGNED DEFAULT NULL,
  `NickName` char(20) NOT NULL,
  `Breed` char(30) NOT NULL,
  `ArrivalDate` date NOT NULL,
  `InHere` tinyint(1) UNSIGNED NOT NULL,
  `FMLNameOfOwner` char(40) DEFAULT NULL,
  `OwnerPhone` char(11) DEFAULT NULL,
  `OwnerAddress` char(80) DEFAULT NULL,
  `DeliveryDate` date DEFAULT NULL,
  `PetPhoto` char(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `animals`
--

INSERT INTO `animals` (`Id_Animals`, `Species`, `NickName`, `Breed`, `ArrivalDate`, `InHere`, `FMLNameOfOwner`, `OwnerPhone`, `OwnerAddress`, `DeliveryDate`, `PetPhoto`) VALUES
(1, 2, 'Машка', 'Мэйнкун', '2018-10-02', 0, 'Небольсина М.', '8945123546', 'ул. Пожарника, д. 11', '2018-10-03', 'ftp://127.0.0.1/1491574272_003-1.jpg'),
(2, 1, 'Добби', 'Пинчер', '2018-10-01', 0, 'Роман Е.В.', '8962804300', 'г. Новоалтайск, ул. Партизанская, д. 110, к.10', '2018-10-04', 'ftp://127.0.0.1/doberman_11.jpg'),
(3, 1, 'Цезарь', 'Дворняга', '2018-10-05', 0, 'Роман Е.В.', '895', 'ыва', '2018-10-04', 'ftp://127.0.0.1/mops_06.jpg'),
(7, 1, 'Шелдон', 'Лабрадор', '2018-09-26', 1, NULL, NULL, NULL, '0001-01-01', 'ftp://127.0.0.1/labrador-retriver.jpg');

-- --------------------------------------------------------

--
-- Структура таблицы `debitcredit`
--

CREATE TABLE `debitcredit` (
  `Id_DebitCredit` int(10) UNSIGNED NOT NULL,
  `GoodsName` int(10) UNSIGNED DEFAULT NULL,
  `Comment` text,
  `Date` date NOT NULL,
  `Debit` tinyint(1) UNSIGNED NOT NULL,
  `Credit` tinyint(1) UNSIGNED NOT NULL,
  `PatientId` int(10) UNSIGNED DEFAULT NULL,
  `UserId` int(10) UNSIGNED DEFAULT NULL,
  `GoodsType` int(10) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `debitcredit`
--

INSERT INTO `debitcredit` (`Id_DebitCredit`, `GoodsName`, `Comment`, `Date`, `Debit`, `Credit`, `PatientId`, `UserId`, `GoodsType`) VALUES
(1, 1, NULL, '2018-10-16', 0, 0, NULL, NULL, NULL),
(2, 1, NULL, '2018-10-16', 0, 5, NULL, 1, NULL),
(3, 1, NULL, '2018-10-16', 0, 4, NULL, 2, NULL),
(4, 1, NULL, '2018-10-16', 1, 0, NULL, NULL, NULL),
(5, 2, NULL, '2018-10-16', 0, 0, NULL, NULL, NULL),
(6, 2, 'Пожертвование', '2018-10-16', 0, 50, NULL, 1, NULL),
(7, 2, 'На жвачку потратил', '2018-10-16', 10, 0, NULL, 1, NULL),
(8, 2, NULL, '2018-10-16', 0, 20, NULL, NULL, NULL),
(9, 2, 'Собачкам на еду', '2018-10-16', 0, 60, NULL, 2, NULL),
(10, 1, 'В попу', '2018-11-04', 1, 0, 2, 1, NULL),
(11, 3, 'Чтоб было смешно', '2018-11-05', 1, 0, 1, 1, NULL),
(12, 4, 'Чтоб было смешно', '2018-11-05', 1, 0, 1, 1, NULL),
(13, 4, 'Ещё смешнее', '2018-11-05', 1, 0, 1, 1, NULL),
(14, 4, 'АХАХ', '2018-11-05', 1, 0, 1, 1, NULL),
(15, 4, '[f[f', '2018-11-05', 1, 0, 1, 1, NULL),
(16, 4, 'Чтобы было смешно', '2018-11-05', 0, 20, NULL, 3, NULL),
(17, 4, 'От Саши', '2018-11-05', 1, 0, 1, 1, NULL),
(18, NULL, 'Почесал собачек за ушками', '2018-11-05', 0, 0, NULL, 1, NULL),
(19, NULL, 'Почесал кошечек', '2018-11-05', 0, 0, NULL, 1, NULL),
(20, NULL, 'Всех почесал', '2018-11-05', 0, 0, NULL, 1, NULL),
(21, 4, 'Так надо', '2018-11-05', 30, 0, NULL, 1, NULL),
(22, 4, 'За знакомство', '2018-11-06', 1, 0, NULL, 1, NULL),
(23, 4, 'От бешенства', '2018-11-06', 1, 0, 2, 1, NULL),
(24, 3, 'Хобана', '2018-11-06', 1, 0, 2, 1, NULL),
(25, 3, 'Плановая вакцинация', '2018-11-06', 1, 0, 3, 1, NULL),
(26, 3, 'От бешенства', '2018-11-06', 1, 0, 7, 1, NULL),
(28, 3, 'Плановая вакцинация', '2018-11-06', 1, 0, NULL, 1, NULL),
(29, 4, 'фывафыва', '2018-11-06', 1, 0, NULL, 1, NULL),
(31, 3, 'asdf', '2018-11-06', 1, 0, NULL, 1, NULL),
(32, 3, 'Для мишутки', '2018-11-06', 1, 0, NULL, 1, NULL),
(41, 4, 'dfsdfsd', '2018-11-11', 1, 0, 7, 1, NULL),
(42, 4, 'fgd', '2018-11-11', 1, 0, 3, 1, NULL),
(43, 4, 'asdas', '2018-11-11', 1, 0, 3, 1, NULL),
(44, 4, 'Лекарство', '2018-11-11', 1, 0, 2, 1, NULL),
(45, NULL, 'Просто помощь', '2018-11-11', 0, 0, NULL, 1, NULL),
(46, NULL, NULL, '2018-11-11', 0, 91, NULL, 3, NULL),
(47, NULL, NULL, '2018-11-11', 0, 91, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `goods`
--

CREATE TABLE `goods` (
  `Id_Goods` int(10) UNSIGNED NOT NULL,
  `NameOfGoods` char(30) NOT NULL,
  `Type` int(10) UNSIGNED DEFAULT NULL,
  `Amount` int(11) NOT NULL,
  `Required` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `goods`
--

INSERT INTO `goods` (`Id_Goods`, `NameOfGoods`, `Type`, `Amount`, `Required`) VALUES
(1, 'Икра чёрная', 1, 8, 10),
(2, 'Рубли', 3, 90, 100),
(3, 'Викодин', 2, 8, 20),
(4, 'Валерьянка', 2, 11, 20);

-- --------------------------------------------------------

--
-- Структура таблицы `goodstype`
--

CREATE TABLE `goodstype` (
  `Id_GoodsType` int(10) UNSIGNED NOT NULL,
  `TypeOfGoods` char(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `goodstype`
--

INSERT INTO `goodstype` (`Id_GoodsType`, `TypeOfGoods`) VALUES
(1, 'Еда'),
(2, 'Лекарства'),
(3, 'Деньги'),
(4, 'Прочее');

-- --------------------------------------------------------

--
-- Структура таблицы `positions`
--

CREATE TABLE `positions` (
  `Id_Positions` int(10) UNSIGNED NOT NULL,
  `NameOfPosition` char(30) NOT NULL,
  `Rights` int(1) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `positions`
--

INSERT INTO `positions` (`Id_Positions`, `NameOfPosition`, `Rights`) VALUES
(1, 'Волонтёр', 0),
(2, 'Джуниор', 1),
(3, 'Миддл', 2),
(4, 'Сеньор', 3);

-- --------------------------------------------------------

--
-- Структура таблицы `species`
--

CREATE TABLE `species` (
  `Id_Species` int(10) UNSIGNED NOT NULL,
  `NameOfSpecies` char(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `species`
--

INSERT INTO `species` (`Id_Species`, `NameOfSpecies`) VALUES
(1, 'Собака'),
(2, 'Кошка');

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `Id_Users` int(10) UNSIGNED NOT NULL,
  `Login` char(20) NOT NULL,
  `Password` char(32) DEFAULT NULL,
  `FirstMiddleLastName` char(40) NOT NULL,
  `Position` int(1) UNSIGNED DEFAULT NULL,
  `Phone` char(11) NOT NULL,
  `Address` char(80) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`Id_Users`, `Login`, `Password`, `FirstMiddleLastName`, `Position`, `Phone`, `Address`) VALUES
(1, 'DNH', '9FF7FCE8DC408BF90D743A79F825CCDC', 'Фещенко Дмитрий Николаевич', 4, '89628043000', 'г. Барнаул, ул. Некрасова, д.41, к.55'),
(2, 'REV', NULL, 'Роман Екатерина Валериевна', 1, '89233421816', 'г. Новоалтайск, ул. Партизанская, д.111, к. 10'),
(3, 'Aldemm', NULL, 'Деменко Александр Михайлович', 1, 'sony', 'пос. Южный, ul. Unknownaya, d. -1'),
(5, 'asdljk', 'D41D8CD98F00B204E9800998ECF8427E', 'a s d', 2, 'as', 'asd');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `animals`
--
ALTER TABLE `animals`
  ADD PRIMARY KEY (`Id_Animals`),
  ADD KEY `Species` (`Species`);

--
-- Индексы таблицы `debitcredit`
--
ALTER TABLE `debitcredit`
  ADD PRIMARY KEY (`Id_DebitCredit`),
  ADD KEY `GoodsName` (`GoodsName`) USING BTREE,
  ADD KEY `debitcredit_ibfk_1` (`PatientId`),
  ADD KEY `debitcredit_ibfk_2` (`UserId`),
  ADD KEY `debitcredit_ibfk_4` (`GoodsType`);

--
-- Индексы таблицы `goods`
--
ALTER TABLE `goods`
  ADD PRIMARY KEY (`Id_Goods`),
  ADD KEY `Type` (`Type`);

--
-- Индексы таблицы `goodstype`
--
ALTER TABLE `goodstype`
  ADD PRIMARY KEY (`Id_GoodsType`);

--
-- Индексы таблицы `positions`
--
ALTER TABLE `positions`
  ADD PRIMARY KEY (`Id_Positions`);

--
-- Индексы таблицы `species`
--
ALTER TABLE `species`
  ADD PRIMARY KEY (`Id_Species`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id_Users`),
  ADD KEY `Position` (`Position`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `animals`
--
ALTER TABLE `animals`
  MODIFY `Id_Animals` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT для таблицы `debitcredit`
--
ALTER TABLE `debitcredit`
  MODIFY `Id_DebitCredit` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=49;

--
-- AUTO_INCREMENT для таблицы `goods`
--
ALTER TABLE `goods`
  MODIFY `Id_Goods` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `goodstype`
--
ALTER TABLE `goodstype`
  MODIFY `Id_GoodsType` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `positions`
--
ALTER TABLE `positions`
  MODIFY `Id_Positions` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `species`
--
ALTER TABLE `species`
  MODIFY `Id_Species` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `Id_Users` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `animals`
--
ALTER TABLE `animals`
  ADD CONSTRAINT `animals_ibfk_1` FOREIGN KEY (`Species`) REFERENCES `species` (`Id_Species`) ON DELETE SET NULL ON UPDATE NO ACTION;

--
-- Ограничения внешнего ключа таблицы `debitcredit`
--
ALTER TABLE `debitcredit`
  ADD CONSTRAINT `debitcredit_ibfk_1` FOREIGN KEY (`PatientId`) REFERENCES `animals` (`Id_Animals`) ON DELETE SET NULL ON UPDATE NO ACTION,
  ADD CONSTRAINT `debitcredit_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id_Users`) ON DELETE SET NULL ON UPDATE NO ACTION,
  ADD CONSTRAINT `debitcredit_ibfk_3` FOREIGN KEY (`GoodsName`) REFERENCES `goods` (`Id_Goods`) ON DELETE SET NULL ON UPDATE NO ACTION,
  ADD CONSTRAINT `debitcredit_ibfk_4` FOREIGN KEY (`GoodsType`) REFERENCES `goodstype` (`Id_GoodsType`) ON DELETE SET NULL ON UPDATE NO ACTION;

--
-- Ограничения внешнего ключа таблицы `goods`
--
ALTER TABLE `goods`
  ADD CONSTRAINT `goods_ibfk_1` FOREIGN KEY (`Type`) REFERENCES `goodstype` (`Id_GoodsType`) ON DELETE SET NULL ON UPDATE NO ACTION;

--
-- Ограничения внешнего ключа таблицы `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`Position`) REFERENCES `positions` (`Id_Positions`) ON DELETE SET NULL ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
