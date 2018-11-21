-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1
-- Время создания: Ноя 20 2018 г., 07:35
-- Версия сервера: 10.1.25-MariaDB
-- Версия PHP: 5.6.31

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
CREATE DATABASE IF NOT EXISTS `petshelter` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `petshelter`;

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

-- --------------------------------------------------------

--
-- Структура таблицы `debitcredit`
--

CREATE TABLE `debitcredit` (
  `Id_DebitCredit` int(10) UNSIGNED NOT NULL,
  `GoodsName` int(10) UNSIGNED DEFAULT NULL,
  `Comment` text,
  `Date` date NOT NULL,
  `Debit` int(11) UNSIGNED NOT NULL,
  `Credit` int(11) UNSIGNED NOT NULL,
  `PatientId` int(10) UNSIGNED DEFAULT NULL,
  `UserId` int(10) UNSIGNED DEFAULT NULL,
  `GoodsType` int(10) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
(1, 'Деньги', 3, 0, 0);

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
(1, 'admin', '21232f297a57a5a743894a0e4a801fc3', '', 4, '', '');

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
  ADD KEY `debitcredit_ibfk_2` (`UserId`);

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
  MODIFY `Id_Animals` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT для таблицы `debitcredit`
--
ALTER TABLE `debitcredit`
  MODIFY `Id_DebitCredit` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT для таблицы `goods`
--
ALTER TABLE `goods`
  MODIFY `Id_Goods` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
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
  MODIFY `Id_Users` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
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
  ADD CONSTRAINT `debitcredit_ibfk_3` FOREIGN KEY (`GoodsName`) REFERENCES `goods` (`Id_Goods`) ON DELETE SET NULL ON UPDATE NO ACTION;

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
