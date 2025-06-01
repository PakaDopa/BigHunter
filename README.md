# 🦣 Unity Prototype - Big Hunter Inspired

## 📦 Unity Version

- Unity `6000.0.37f1`

---

## 🎮 Reference Game

- **Original Game**: [Big Hunter by Kakarod Interactive](https://play.google.com/store/apps/details?id=com.kakarod.hunter)
- **게임 특징 요약**:
  - 투창 기반의 물리 퍼즐 액션
  - 리듬감 있는 공격 + 포물선 투척 시스템
  - 부위별 크리티컬 판정 및 방어 부위 존재
  - 간결한 2D 스타일 + 다이나믹한 연출

---

## 🎮 Version 1.0.0 - prototype
** !PC환경에서 테스트 해보실 수 있습니다. (UI 등 해상도는 모바일에 대응했습니다.)
### 실행 파일
- [google drive](https://drive.google.com/file/d/1Sar4tWTFjnBMdavdDWCvuu8Q3j9d3s_9/view?usp=drive_link)

### 시연 영상
[![ProtoHunter 시연 영상](https://img.youtube.com/vi/dboh5aAAP3Q/0.jpg)](https://youtu.be/dboh5aAAP3Q)

### 구현 목록

** 애니메이션 **
1. 플레이어의 이동, 투척 준비, 투척, 죽음 애니메이션 sprite bone을 활용하여 제작
2. 적의 이동, 공격 패턴, 죽음 애니메이션 sprite bone을 활용하여 제작

** 물리 **
1. rigidybody2D를 활용한 창 궤적, 던지기 & 적에게 꽂히는 것 구현
2. 방패에 맞을 시 창 튕겨나감

** 시스템 **
1. 적의 부위 별로 리액션(데미지, 튕겨내기) 구현 -> 몸통은 30, 머리는 45, 방패를 맞출 시 튕겨나감
2. 머리에 일정 데미지가 누적되면 방패를 들어올리는 패턴 (추가 예정)
3. 싱글톤 기반 각종 매니저 개발
4. FSM 개발

